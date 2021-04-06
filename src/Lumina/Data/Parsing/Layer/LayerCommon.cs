using System;
using System.Diagnostics;
using System.IO;
using Lumina.Extensions;
using Vector3 = Lumina.Data.Parsing.Common.Vector3;
using Transformation = Lumina.Data.Parsing.Common.Transformation;
// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedField.Local

// x field is never used warning
#pragma warning disable 169
#pragma warning disable 414

namespace Lumina.Data.Parsing.Layer
{
    public class LayerCommon
    {
        public interface IInstanceObject
        {
            //TODO consider refactoring to classes for real inheritance
//            IInstanceObject Read();
        }

        public struct LayerChunk
        {
            public char[] ChunkId; //[4]
            public int ChunkSize;
            public int LayerGroupId;
            public string Name;
            public int Layers;
            public int LayersCount;


            public static LayerChunk Read( BinaryReader br )
            {
                LayerChunk ret = new LayerChunk();

                ret.ChunkId = br.ReadChars( 4 );
                ret.ChunkSize = br.ReadInt32();

                long start = br.BaseStream.Position;
                ret.LayerGroupId = br.ReadInt32();
                ret.Name = br.ReadStringOffset( start );
                ret.Layers = br.ReadInt32();
                ret.LayersCount = br.ReadInt32();

                return ret;
            }
        }

        /* Base classes */
        public struct TriggerBoxInstanceObject
        {
	        public TriggerBoxShape TriggerBoxShape;
	        public short Priority;
            public byte Enabled;
            private byte _padding0;
            private uint _padding1;

            public static TriggerBoxInstanceObject Read( BinaryReader br )
            {
                return new TriggerBoxInstanceObject
                {
                    TriggerBoxShape = (TriggerBoxShape)br.ReadInt32(),
                    Priority = br.ReadInt16(),
                    Enabled = br.ReadByte(),
                    _padding0 = br.ReadByte(),
                    _padding1 = br.ReadUInt32()
                };
            }
        }

        public struct GameInstanceObject
        {
            public uint BaseId;

            public static GameInstanceObject Read( BinaryReader br )
            {
                return new GameInstanceObject() { BaseId = br.ReadUInt32() };
            }
        }

        public struct NPCInstanceObject
        {
            public GameInstanceObject ParentData;

            public uint PopWeather;
            public byte PopTimeStart;
            public byte PopTimeEnd;
            public byte[] Padding00; //[2]
            public uint MoveAi;
            public byte WanderingRange;
            public byte Route;
            public ushort EventGroup;
            private uint _padding1;
            private uint _padding2;

            public static NPCInstanceObject Read( BinaryReader br )
            {
                return new NPCInstanceObject
                {
                    ParentData = GameInstanceObject.Read( br ),
                    PopWeather = br.ReadUInt32(),
                    PopTimeStart = br.ReadByte(),
                    PopTimeEnd = br.ReadByte(),
                    Padding00 = br.ReadBytes( 2 ),
                    MoveAi = br.ReadUInt32(),
                    WanderingRange = br.ReadByte(),
                    Route = br.ReadByte(),
                    EventGroup = br.ReadUInt16(),
                    _padding1 = br.ReadUInt32(),
                    _padding2 = br.ReadUInt32()
                };
            }
        }

        public struct CollisionAttribute
        {
            public byte CollisionExist;
            public byte AttributeEnable;
            public byte[] Padding00; //[2]
            public uint IdPallet;
            public uint EnvSet;
            public byte NaviMeshCollisionDisabled;
            public byte WaterSurface;
            public byte CameraCollision;
            public byte CharacterCollision;
            public byte EyesCollision;
            public byte Fishing;
            public byte Sheat;
            public byte Chocobo;
            public byte Gimmick;
            public byte Room;
            public byte Table;
            public byte Wall;

            public static CollisionAttribute Read( BinaryReader br )
            {
                return new CollisionAttribute()
                {
                    CollisionExist = br.ReadByte(),
                    AttributeEnable = br.ReadByte(),
                    Padding00 = br.ReadBytes( 2 ),
                    IdPallet = br.ReadUInt32(),
                    EnvSet = br.ReadUInt32(),
                    NaviMeshCollisionDisabled = br.ReadByte(),
                    WaterSurface = br.ReadByte(),
                    CameraCollision = br.ReadByte(),
                    CharacterCollision = br.ReadByte(),
                    EyesCollision = br.ReadByte(),
                    Fishing = br.ReadByte(),
                    Sheat = br.ReadByte(),
                    Chocobo = br.ReadByte(),
                    Gimmick = br.ReadByte(),
                    Room = br.ReadByte(),
                    Table = br.ReadByte(),
                    Wall = br.ReadByte()
                };
            }
        }

        struct CreationParamsBase
        {
            public byte Enabled;
            public byte[] Padding00; //[3]
            private uint _padding1;
            private uint _padding2;

            public static CreationParamsBase Read( BinaryReader br )
            {
                CreationParamsBase ret = new CreationParamsBase();

                ret.Enabled = br.ReadByte();
                ret.Padding00 = br.ReadBytes( 3 );
                ret._padding1 = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct CreationParams// : CreationParamsBase
        {
            public CollisionAttribute CollisionAttribute;

            public static CreationParams Read( BinaryReader br )
            {
                return new CreationParams { CollisionAttribute = CollisionAttribute.Read( br ) };
            }
        }

        public struct PathControlPoint
        {
            public Vector3 Translation;
            public ushort PointId;
            public byte Select;
            public byte Padding00;

            public static PathControlPoint Read( BinaryReader br )
            {
                PathControlPoint ret = new PathControlPoint();

                ret.Translation = Vector3.Read( br );
                ret.PointId = br.ReadUInt16();
                ret.Select = br.ReadByte();
                ret.Padding00 = br.ReadByte();

                return ret;
            }
        }

        public struct PathInstanceObject
        {
            public int ControlPoints;
            public int ControlPointCount;
            private uint _padding0;
            private uint _padding1;

            public PathControlPoint[] ControlPointsArray;

            public static PathInstanceObject Read( BinaryReader br )
            {
                PathInstanceObject ret = new PathInstanceObject();

                // todo: pass baseoffsets of instanceobjects to all composition classes
                long start = br.BaseStream.Position - 48;

                ret.ControlPoints = br.ReadInt32();
                ret.ControlPointCount = br.ReadInt32();
                ret._padding0 = br.ReadUInt32();
                ret._padding1 = br.ReadUInt32();
                long end = br.BaseStream.Position;

                ret.ControlPointsArray = new PathControlPoint[ret.ControlPointCount];

                br.BaseStream.Position = start + ret.ControlPoints;
                for( int i = 0; i < ret.ControlPointCount; i++ )
                    ret.ControlPointsArray[ i ] = PathControlPoint.Read( br );
                br.BaseStream.Position = end;

                return ret;
            }
        }

        /* Composition clases */
        public struct RelativePositions
        {
            public int Pos;
            public int PosCount;

            public static RelativePositions Read( BinaryReader br )
            {
                RelativePositions ret = new RelativePositions();

                ret.Pos = br.ReadInt32();
                ret.PosCount = br.ReadInt32();

                return ret;
            }
        }

        public struct WeaponModel
        {
            public ushort SkeletonId;
            public ushort PatternId;
            public ushort ImageChangeId;
            public ushort StainingId;

            public static WeaponModel Read( BinaryReader br )
            {
                WeaponModel ret = new WeaponModel();

                ret.SkeletonId = br.ReadUInt16();
                ret.PatternId = br.ReadUInt16();
                ret.ImageChangeId = br.ReadUInt16();
                ret.StainingId = br.ReadUInt16();

                return ret;
            }
        }

        public struct ColorHDRI
        {
            public byte Red;
            public byte Green;
            public byte Blue;
            public byte Alpha;
            public float Intensity;

            public static ColorHDRI Read( BinaryReader br )
            {
                ColorHDRI ret = new ColorHDRI();

                ret.Red = br.ReadByte();
                ret.Green = br.ReadByte();
                ret.Blue = br.ReadByte();
                ret.Alpha = br.ReadByte();
                ret.Intensity = br.ReadSingle();

                return ret;
            }
        }

        public struct Color
        {
            public byte Red;
            public byte Green;
            public byte Blue;
            public byte Alpha;

            public static Color Read( BinaryReader br )
            {
                Color ret = new Color();

                ret.Red = br.ReadByte();
                ret.Green = br.ReadByte();
                ret.Blue = br.ReadByte();
                ret.Alpha = br.ReadByte();

                return ret;
            }
        }

        public struct MovePathSettings
        {
	        public MovePathMode Mode;
            public byte AutoPlay;
            public byte Padding00;
            public ushort Time;
            public byte Loop;
            public byte Reverse;
            private byte[] _padding01; //[2]
            public RotationType Rotation;
            public ushort AccelerateTime;
            public ushort DecelerateTime;
            public float[] VerticalSwingRange; //[2]
            public float[] HorizontalSwingRange; //[2]
            public float[] SwingMoveSpeedRange; //[2]
            public float[] SwingRotation; //[2]
            public float[] SwingRotationSpeedRange; //[2]

            public static MovePathSettings Read( BinaryReader br )
            {
                MovePathSettings ret = new MovePathSettings();

                ret.Mode = (MovePathMode) br.ReadInt32();
                ret.AutoPlay = br.ReadByte();
                ret.Padding00 = br.ReadByte();
                ret.Time = br.ReadUInt16();
                ret.Loop = br.ReadByte();
                ret.Reverse = br.ReadByte();
                ret._padding01 = br.ReadBytes( 2 );
                ret.Rotation = (RotationType) br.ReadUInt32();
                ret.AccelerateTime = br.ReadUInt16();
                ret.DecelerateTime = br.ReadUInt16();

                // TODO: verify this works lol
                ret.VerticalSwingRange = br.ReadStructures< float >( 2 ).ToArray();
                ret.HorizontalSwingRange = br.ReadStructures< float >( 2 ).ToArray();
                ret.SwingMoveSpeedRange = br.ReadStructures< float >( 2 ).ToArray();
                ret.SwingRotation = br.ReadStructures< float >( 2 ).ToArray();
                ret.SwingRotationSpeedRange = br.ReadStructures< float >( 2 ).ToArray();

                return ret;
            }
        }

        public struct SEParam
        {
            public SoundEffectType SoundEffectType;
            public byte AutoPlay;
            public byte IsNoFarClip;
            private byte[] _padding00; //[2]
            public int Binary;
            public int BinaryCount;
            public uint PointSelection;

            public byte[] Binaries;

            public static SEParam Read( BinaryReader br )
            {
                SEParam ret = new SEParam();

                long start = br.BaseStream.Position;
                ret.SoundEffectType = (SoundEffectType) br.ReadInt32();
                ret.AutoPlay = br.ReadByte();
                ret.IsNoFarClip = br.ReadByte();
                ret._padding00 = br.ReadBytes( 2 );
                ret.Binary = br.ReadInt32();
                ret.BinaryCount = br.ReadInt32();
                ret.PointSelection = br.ReadUInt32();
                long end = br.BaseStream.Position;

                br.BaseStream.Position = start + ret.Binary;
                ret.Binaries = br.ReadBytes( ret.BinaryCount );
                br.BaseStream.Position = end;

                return ret;
            }
        }

        public struct SGMemberID
        {
            public byte[] MemberIDs;// [4]

            public static SGMemberID Read( BinaryReader br )
            {
                SGMemberID ret = new SGMemberID();

                ret.MemberIDs = br.ReadBytes( 4 );

                return ret;
            }
        }

        public struct SGOverriddenMember
        {
	        public int AssetType;
	        public SGMemberID MemberId;

            public static SGOverriddenMember Read( BinaryReader br )
            {
                SGOverriddenMember ret = new SGOverriddenMember();

                ret.AssetType = br.ReadInt32();
                ret.MemberId = SGMemberID.Read( br );

                return ret;
            }
        }

        struct SGOverriddenBG
        {
	        byte RenderShadowEnabled;
	        byte RenderLightShadowEnabled;
            // fixed byte Padding00[2];
	        float RenderModelClipRange;
	        byte IsVisible;
	        byte CollisionExist;
            // fixed byte Padding01[2];
        }

        struct SGOverriddenVFX
        {
	        byte ColorEnable;
	        Color color;
	        byte IsAutoPlay;
	        byte ZCorrectEnable;
            private byte Padding00;
	        float ZCorrect;
        }

        struct SGOverriddenSE
        {
	        byte AutoPlay;
            // fixed byte Padding00[3];
        }

        struct SGOverriddenLight
        {
	        ColorHDRI color;
	        float ShadowClipRange;
	        byte SpecularEnabled;
	        byte BGShadowEnabled;
	        byte CharacterShadowEnabled;
            byte Padding00;
	        ushort MergeGroupID;
	        byte DiffuseColorHDREdited;
	        byte ShadowClipRangeEdited;
	        byte SpecularEnabledEdited;
	        byte BGShadowEnabledEdited;
	        byte CharacterShadowEnabledEdited;
	        byte MergeGroupIDEdited;
        }

        /* Instance object classes */
        public struct BGInstanceObject : IInstanceObject
        {
            public string AssetPath;
            public string CollisionAssetPath;

            public ModelCollisionType CollisionType;
            public uint AttributeMask;
            public uint Attribute;
            public int CollisionConfig;   //TODO: read CollisionConfig
            public byte IsVisible;
            public byte RenderShadowEnabled;
            public byte RenderLightShadowEnabled;
            public byte Padding00;
            public float RenderModelClipRange;

            public static BGInstanceObject Read( BinaryReader br )
            {
                BGInstanceObject ret = new BGInstanceObject();

                long start = br.BaseStream.Position;
                ret.AssetPath = br.ReadStringOffset( start - 48 );
                ret.CollisionAssetPath = br.ReadStringOffset( start - 48 );
                ret.CollisionType = (ModelCollisionType) br.ReadInt32();
                ret.AttributeMask = br.ReadUInt32();
                ret.Attribute = br.ReadUInt32();
                ret.CollisionConfig = br.ReadInt32();
                ret.IsVisible = br.ReadByte();
                ret.RenderShadowEnabled = br.ReadByte();
                ret.RenderLightShadowEnabled = br.ReadByte();
                ret.Padding00 = br.ReadByte();
                ret.RenderModelClipRange = br.ReadSingle();

                return ret;
            }
        }

        public struct LightInstanceObject : IInstanceObject
        {
            public LightType LightType;
            public float Attenuation;
            public float RangeRate;
            public PointLightType PointLightType;
            public float AttenuationConeCoefficient;
            public float ConeDegree;
            public string TexturePath;
            public ColorHDRI DiffuseColorHDRI;
            public byte FollowsDirectionalLight;
            private byte _padding1;
            private short _padding2;
            public byte SpecularEnabled;
            public byte BGShadowEnabled;
            public byte CharacterShadowEnabled;
            private byte _padding3;
            public float ShadowClipRange;
            public float PlaneLightRotationX;
            public float PlaneLightRotationY;
            public ushort MergeGroupID;
            private byte _padding4;


            public static LightInstanceObject Read( BinaryReader br )
            {
                LightInstanceObject ret = new LightInstanceObject();

                long start = br.BaseStream.Position;
                ret.LightType = (LightType) br.ReadInt32();
                ret.Attenuation = br.ReadSingle();
                ret.RangeRate = br.ReadSingle();
                ret.PointLightType = (PointLightType) br.ReadInt32();
                ret.AttenuationConeCoefficient = br.ReadSingle();
                ret.ConeDegree = br.ReadSingle();
                ret.TexturePath = br.ReadStringOffset( start - 48 );
                ret.DiffuseColorHDRI = ColorHDRI.Read( br );
                ret.FollowsDirectionalLight = br.ReadByte();
                ret._padding1 = br.ReadByte();
                ret._padding2 = br.ReadInt16();
                ret.SpecularEnabled = br.ReadByte();
                ret.BGShadowEnabled = br.ReadByte();
                ret.CharacterShadowEnabled = br.ReadByte();
                ret._padding3 = br.ReadByte();
                ret.ShadowClipRange = br.ReadSingle();
                ret.PlaneLightRotationX = br.ReadSingle();
                ret.PlaneLightRotationY = br.ReadSingle();
                ret.MergeGroupID = br.ReadUInt16();
                ret._padding4 = br.ReadByte();

                return ret;
            }
        }

        public struct VFXInstanceObject : IInstanceObject
        {
            public string AssetPath;
            public float SoftParticleFadeRange;
            private uint _padding2;
            public Color Color;
            public byte IsAutoPlay;
            public byte IsNoFarClip;
            private byte[] _padding00; //[2]
            public float FadeNearStart;
            public float FadeNearEnd;
            public float FadeFarStart;
            public float FadeFarEnd;
            public float ZCorrect;


            public static VFXInstanceObject Read( BinaryReader br )
            {
                VFXInstanceObject ret = new VFXInstanceObject();

                long start = br.BaseStream.Position;
                ret.AssetPath = br.ReadStringOffset( start - 48 );
                ret.SoftParticleFadeRange = br.ReadSingle();
                ret._padding2 = br.ReadUInt32();
                ret.Color = Color.Read( br );
                ret.IsAutoPlay = br.ReadByte();
                ret.IsNoFarClip = br.ReadByte();
                ret._padding00 = br.ReadBytes( 2 );
                ret.FadeNearStart = br.ReadSingle();
                ret.FadeNearEnd = br.ReadSingle();
                ret.FadeFarStart = br.ReadSingle();
                ret.FadeFarEnd = br.ReadSingle();
                ret.ZCorrect = br.ReadSingle();

                return ret;
            }
        }

        public struct PositionMarkerInstanceObject : IInstanceObject
        {
            public PositionMarkerType PositionMarkerType;
            public int CommentJP; //TODO: are these strings?
            public int CommentEN;

            public static PositionMarkerInstanceObject Read( BinaryReader br )
            {
                PositionMarkerInstanceObject ret = new PositionMarkerInstanceObject();

                ret.PositionMarkerType = (PositionMarkerType) br.ReadInt32();
                ret.CommentJP = br.ReadInt32();
                ret.CommentEN = br.ReadInt32();

                return ret;
            }
        }

        public struct SharedGroupInstanceObject : IInstanceObject
        {
            public string AssetPath;
            public DoorState InitialDoorState;
            public int OverriddenMembers; // TODO
            public int OverriddenMembersCount;
            public RotationState InitialRotationState;
            public byte RandomTimelineAutoPlay;
            public byte RandomTimelineLoopPlayback;
            public byte IsCollisionControllableWithoutEObj;
            private byte _padding00;
            public uint BoundCLientPathInstanceId;
            public int _MovePathSettings;
            public byte NotCreateNavimeshDoor;
            private byte[] _padding01; //[3]
            public TransformState InitialTransformState;
            public ColourState InitialColorState;

            public SGOverriddenMember[] SGOverriddenMembers;
            public MovePathSettings MovePathSettings;

            public static SharedGroupInstanceObject Read( BinaryReader br )
            {
                SharedGroupInstanceObject ret = new SharedGroupInstanceObject();

                long start = br.BaseStream.Position;
                ret.AssetPath = br.ReadStringOffset( start - 48 );
                ret.InitialDoorState = (DoorState) br.ReadInt32();
                ret.OverriddenMembers = br.ReadInt32();
                ret.OverriddenMembersCount = br.ReadInt32();
                ret.InitialRotationState = (RotationState) br.ReadInt32();
                ret.RandomTimelineAutoPlay = br.ReadByte();
                ret.RandomTimelineLoopPlayback = br.ReadByte();
                ret.IsCollisionControllableWithoutEObj = br.ReadByte();
                ret._padding00 = br.ReadByte();
                ret.BoundCLientPathInstanceId = br.ReadUInt32();
                ret._MovePathSettings = br.ReadInt32();
                ret.NotCreateNavimeshDoor = br.ReadByte();
                ret._padding01 = br.ReadBytes( 3 );
                ret.InitialTransformState = (TransformState) br.ReadInt32();
                ret.InitialColorState = (ColourState) br.ReadInt32();
                long end = br.BaseStream.Position;

                //TODO unlock the secrets
//                if( ret.OverriddenMembersCount > 0 )
//                {
//                    ret.SGOverriddenMembers = new SGOverriddenMember[ret.OverriddenMembersCount];
//                    for (int i = 0; i < ret.OverriddenMembersCount; i++)
//                        ret.SGOverriddenMembers[i] = SGOverriddenMember.Read(br);
//                }

                br.BaseStream.Position = start + ret._MovePathSettings - 48;
                ret.MovePathSettings = MovePathSettings.Read( br );
                br.BaseStream.Position = end;

                return ret;
            }
        }

        public struct SoundInstanceObject : IInstanceObject
        {
            public int SoundEffectParam;
            public string AssetPath;
            public SEParam SEParam;

            public static SoundInstanceObject Read( BinaryReader br )
            {
                SoundInstanceObject ret = new SoundInstanceObject();

                long start = br.BaseStream.Position;
                ret.SoundEffectParam = br.ReadInt32();
                ret.AssetPath = br.ReadStringOffset( start - 48 );
                long end = br.BaseStream.Position;

                br.BaseStream.Position = start + ret.SoundEffectParam - 48;
                ret.SEParam = SEParam.Read( br );
                br.BaseStream.Position = end;
                
                return ret;
            }
        }

        public struct ENPCInstanceObject : IInstanceObject
        {
            public NPCInstanceObject ParentData;

            public uint Behavior;
            private uint _padding3;
            private uint _padding4;

            public static ENPCInstanceObject Read( BinaryReader br )
            {
                ENPCInstanceObject ret = new ENPCInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = NPCInstanceObject.Read( br );
                ret.Behavior = br.ReadUInt32();
                ret._padding3 = br.ReadUInt32();
                ret._padding4 = br.ReadUInt32();

                return ret;
            }
        }

        struct BNPCInstanceObject : IInstanceObject
        {
            public NPCInstanceObject ParentData;

            public uint NameId;
            public uint DropItem;
            public float SenseRangeRate;
            public ushort Level;
            public byte ActiveType;
            public byte PopInterval;
            public byte PopRate;
            public byte PopEvent;
            public byte LinkGroup;
            public byte LinkFamily;
            public byte LinkRange;
            public byte LinkCountLimit;
            public byte NonpopInitZone;
            public byte InvalidRepop;
            public byte LinkParent;
            public byte LinkOverride;
            public byte LinkReply;
            public byte Nonpop;
            public RelativePositions _RelativePositions;
            public float HorizontalPopRange;
            public float VerticalPopRange;
            public int BNpcBaseData;
            public byte RepopId;
            public byte BNPCRankId;
            public ushort TerritoryRange;
            public uint BoundInstanceID;
            public uint FateLayoutLabelId;
            public uint NormalAI;
            public uint ServerPathId;
            public uint EquipmentID;
            public uint CustomizeID;

            public Vector3[] RelativePositions;

            public static BNPCInstanceObject Read( BinaryReader br )
            {
                BNPCInstanceObject ret = new BNPCInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = NPCInstanceObject.Read( br );
                ret.NameId = br.ReadUInt32();
                ret.DropItem = br.ReadUInt32();
                ret.SenseRangeRate = br.ReadSingle();
                ret.Level = br.ReadUInt16();
                ret.ActiveType = br.ReadByte();
                ret.PopInterval = br.ReadByte();
                ret.PopRate = br.ReadByte();
                ret.PopEvent = br.ReadByte();
                ret.LinkGroup = br.ReadByte();
                ret.LinkFamily = br.ReadByte();
                ret.LinkRange = br.ReadByte();
                ret.LinkCountLimit = br.ReadByte();
                ret.NonpopInitZone = br.ReadByte();
                ret.InvalidRepop = br.ReadByte();
                ret.LinkParent = br.ReadByte();
                ret.LinkOverride = br.ReadByte();
                ret.LinkReply = br.ReadByte();
                ret.Nonpop = br.ReadByte();
                ret._RelativePositions = LayerCommon.RelativePositions.Read( br );
                ret.HorizontalPopRange = br.ReadSingle();
                ret.VerticalPopRange = br.ReadSingle();
                ret.BNpcBaseData = br.ReadInt32();
                ret.RepopId = br.ReadByte();
                ret.BNPCRankId = br.ReadByte();
                ret.TerritoryRange = br.ReadUInt16();
                ret.BoundInstanceID = br.ReadUInt32();
                ret.FateLayoutLabelId = br.ReadUInt32();
                ret.NormalAI = br.ReadUInt32();
                ret.ServerPathId = br.ReadUInt32();
                ret.EquipmentID = br.ReadUInt32();
                ret.CustomizeID = br.ReadUInt32();
                long end = br.BaseStream.Position;

                ret.RelativePositions = new Vector3[ret._RelativePositions.PosCount];

                br.BaseStream.Position = start + ret._RelativePositions.Pos;
                for( int i = 0; i < ret._RelativePositions.PosCount; i++)
                    ret.RelativePositions[ i ] = Vector3.Read( br );
                br.BaseStream.Position = end;

                return ret;
            }
        }

        // Need to check if even used/if in files
        public struct CTCharacter : IInstanceObject
        {
            public uint Flags;
            public uint ENpcID;
            public uint BNpcID;
            public uint SEPack;
            public int ModelVisibilities;
            public int ModelVisibilitiesCount;
            public int Weapons;
            public int WeaponCount;
            public byte Visible;
            public byte[] Padding00; //[3]

            public static CTCharacter Read( BinaryReader br )
            {
                CTCharacter ret = new CTCharacter();
                long start = br.BaseStream.Position;

                ret.Flags = br.ReadUInt32();
                ret.ENpcID = br.ReadUInt32();
                ret.BNpcID = br.ReadUInt32();
                ret.SEPack = br.ReadUInt32();
                ret.ModelVisibilities = br.ReadInt32();
                ret.ModelVisibilitiesCount = br.ReadInt32();
                ret.Weapons = br.ReadInt32();
                ret.WeaponCount = br.ReadInt32();
                ret.Visible = br.ReadByte();
                ret.Padding00 = br.ReadBytes( 3 );

                return ret;
            }
        }

        public struct AetheryteInstanceObject : IInstanceObject// : GameInstanceObject
        {
            public GameInstanceObject ParentData;

            public uint BoundInstanceID;
            private uint _padding1;

            public static AetheryteInstanceObject Read( BinaryReader br )
            {
                AetheryteInstanceObject ret = new AetheryteInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = GameInstanceObject.Read( br );
                ret.BoundInstanceID = br.ReadUInt32();
                ret._padding1 = br.ReadUInt32();

                return ret;
            }
        }

        public struct EnvSetInstanceObject : IInstanceObject
        {
            public string AssetPath;
            public uint BoundInstanceId;
            public EnvSetShape Shape;
            public byte IsEnvMapShootingPoint;
            public byte Priority;
            private byte[] _padding00; //[2]
            public float EffectiveRange;
            public int InterpolationTime;
            public float Reverb;
            public float Filter;
            public string SoundAssetPath;


            public static EnvSetInstanceObject Read( BinaryReader br )
            {
                EnvSetInstanceObject ret = new EnvSetInstanceObject();
                long start = br.BaseStream.Position;

                ret.AssetPath = br.ReadStringOffset( start - 48 );
                ret.BoundInstanceId = br.ReadUInt32();
                ret.Shape = (EnvSetShape) br.ReadInt32();
                ret.IsEnvMapShootingPoint = br.ReadByte();
                ret.Priority = br.ReadByte();
                ret._padding00 = br.ReadBytes( 2 );
                ret.EffectiveRange = br.ReadSingle();
                ret.InterpolationTime = br.ReadInt32();
                ret.Reverb = br.ReadSingle();
                ret.Filter = br.ReadSingle();
                ret.SoundAssetPath = br.ReadStringOffset( start - 48 );

                return ret;
            }
        }

        // this extends GameInstanceObject but seems to not actually
        public struct GatheringInstanceObject : IInstanceObject// : GameInstanceObject
        {
            public uint GatheringPointId; // maps to GatheringPoint EXD
            private uint _padding2;

            public static GatheringInstanceObject Read( BinaryReader br )
            {
                GatheringInstanceObject ret = new GatheringInstanceObject();
                long start = br.BaseStream.Position;

                ret.GatheringPointId = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct HelperObjInstanceObject : IInstanceObject
        {
            public HelperObjectType ObjType;
            public TargetType TargetTypeBin;
            public byte Specific;
            public CharacterSize CharacterSize;
            public byte UseDefaultMotion;
            public byte PartyMemberIndex;
            public uint TargetInstanceId;
            public uint DirectId;
            public byte UseDirectId;
            public byte KeepHighTexture;
            public WeaponModel Weapon;
            public byte AllianceMemberIndex;
            private byte _padding00;
            public float SkyVisibility;
            public int OtherInstanceObject;
            public byte UseTransform;
            public byte ModelLod;
            public byte TextureLod;
            public DrawHeadParts DrawHeadParts;
            public Transformation DefaultTransform;

            public static HelperObjInstanceObject Read( BinaryReader br )
            {
                HelperObjInstanceObject ret = new HelperObjInstanceObject();
                long start = br.BaseStream.Position;

                ret.ObjType = (HelperObjectType) br.ReadInt32();
                ret.TargetTypeBin = (TargetType) br.ReadInt32();
                ret.Specific = br.ReadByte();
                ret.CharacterSize = (CharacterSize) br.ReadByte();
                ret.UseDefaultMotion = br.ReadByte();
                ret.PartyMemberIndex = br.ReadByte();
                ret.TargetInstanceId = br.ReadUInt32();
                ret.DirectId = br.ReadUInt32();
                ret.UseDirectId = br.ReadByte();
                ret.KeepHighTexture = br.ReadByte();
                ret.Weapon = WeaponModel.Read( br );
                ret.AllianceMemberIndex = br.ReadByte();
                ret._padding00 = br.ReadByte();
                ret.SkyVisibility = br.ReadSingle();
                ret.OtherInstanceObject = br.ReadInt32();
                ret.UseTransform = br.ReadByte();
                ret.ModelLod = br.ReadByte();
                ret.TextureLod = br.ReadByte();
                ret.DrawHeadParts = (DrawHeadParts) br.ReadInt32();
                ret.DefaultTransform = Transformation.Read( br );

                return ret;
            }
        }

        public struct TreasureInstanceObject : IInstanceObject// : GameInstanceObject
        {
            public GameInstanceObject ParentData;

            public byte NonpopInitZone;
            private byte[] _padding00; //[3]
            private uint _padding1;
            private uint _padding2;

            public static TreasureInstanceObject Read( BinaryReader br )
            {
                TreasureInstanceObject ret = new TreasureInstanceObject();
                long start = br.BaseStream.Position;

                ret.NonpopInitZone = br.ReadByte();
                ret._padding00 = br.ReadBytes( 3 );
                ret._padding1 = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct WeaponInstanceObject : IInstanceObject
        {
            public WeaponModel Model;
            public byte IsVisible;
            private byte[] _padding00; //[3]

            public static WeaponInstanceObject Read( BinaryReader br )
            {
                WeaponInstanceObject ret = new WeaponInstanceObject();
                long start = br.BaseStream.Position;

                ret.Model = WeaponModel.Read( br );
                ret.IsVisible = br.ReadByte();
                ret._padding00 = br.ReadBytes( 3 );

                return ret;
            }
        }

        public struct PopRangeInstanceObject : IInstanceObject
        {
            public PopType PopType;
            public RelativePositions _RelativePositions;
            public float InnerRadiusRatio;
            public byte Index;
            private byte[] _padding00; //[3]
            private uint _padding1;

            public Vector3[] RelativePositions;

            public static PopRangeInstanceObject Read( BinaryReader br )
            {
                PopRangeInstanceObject ret = new PopRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret.PopType = (PopType) br.ReadInt32();
                ret._RelativePositions = LayerCommon.RelativePositions.Read( br );
                ret.InnerRadiusRatio = br.ReadSingle();
                ret.Index = br.ReadByte();
                ret._padding00 = br.ReadBytes( 3 );
                ret._padding1 = br.ReadUInt32();
                long end = br.BaseStream.Position;

                ret.RelativePositions = new Vector3[ret._RelativePositions.PosCount];

                br.BaseStream.Position = start + ret._RelativePositions.Pos - 48;
                for (int i = 0; i < ret._RelativePositions.PosCount; i++)
                    ret.RelativePositions[i] = Vector3.Read( br );
                br.BaseStream.Position = end;

                return ret;
            }
        }

        public struct ExitRangeInstanceObject : IInstanceObject// : TriggerBoxInstanceObject
        {
            public TriggerBoxInstanceObject ParentData;

            public ExitType ExitType;
            public ushort ZoneId;
            public ushort TerritoryType;
            public int Index;
            public uint DestInstanceId;
            public uint ReturnInstanceId;
            public float PlayerRunningDirection;
            private uint _padding3;

            public static ExitRangeInstanceObject Read( BinaryReader br )
            {
                ExitRangeInstanceObject ret = new ExitRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = TriggerBoxInstanceObject.Read( br );
                ret.ExitType = (ExitType) br.ReadInt32();
                ret.ZoneId = br.ReadUInt16();
                ret.TerritoryType = br.ReadUInt16();
                ret.Index = br.ReadInt32();
                ret.DestInstanceId = br.ReadUInt32();
                ret.ReturnInstanceId = br.ReadUInt32();
                ret.PlayerRunningDirection = br.ReadSingle();
                ret._padding3 = br.ReadUInt32();

                return ret;
            }
        }

        public struct MapRangeInstanceObject : IInstanceObject// : TriggerBoxInstanceObject
        {
            public TriggerBoxInstanceObject ParentData;

            public uint Map;
	        public uint PlaceNameBlock;
            public uint PlaceNameSpot;
            public uint Weather;
            public uint BGM; // links to BGMSituation EXD
            private uint _padding2;
            private uint _padding3;
            private ushort _padding4;
            public byte HousingBlockId;
            public byte RestBonusEffective;
            public byte DiscoveryId;
            public byte MapEnabled;
            public byte PlaceNameEnabled;
            public byte DiscoveryEnabled;
            public byte BGMEnabled;
            public byte WeatherEnabled;
            public byte RestBonusEnabled;
            public byte BGMPlayZoneInOnly;
            public byte LiftEnabled;
            public byte HousingEnabled;
            private byte[] _padding01; //[2]

            public static MapRangeInstanceObject Read( BinaryReader br )
            {
                MapRangeInstanceObject ret = new MapRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = TriggerBoxInstanceObject.Read( br );
                ret.Map = br.ReadUInt32();
                ret.PlaceNameBlock = br.ReadUInt32();
                ret.PlaceNameSpot = br.ReadUInt32();
                ret.Weather = br.ReadUInt32();
                ret.BGM = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();
                ret._padding3 = br.ReadUInt32();
                ret._padding4 = br.ReadUInt16();
                ret.HousingBlockId = br.ReadByte();
                ret.RestBonusEffective = br.ReadByte();
                ret.DiscoveryId = br.ReadByte();
                ret.MapEnabled = br.ReadByte();
                ret.PlaceNameEnabled = br.ReadByte();
                ret.DiscoveryEnabled = br.ReadByte();
                ret.BGMEnabled = br.ReadByte();
                ret.WeatherEnabled = br.ReadByte();
                ret.RestBonusEnabled = br.ReadByte();
                ret.BGMPlayZoneInOnly = br.ReadByte();
                ret.LiftEnabled = br.ReadByte();
                ret.HousingEnabled = br.ReadByte();
                ret._padding01 = br.ReadBytes( 2 );

                return ret;
            }
        }

        public struct EventInstanceObject : IInstanceObject
        {
            public GameInstanceObject ParentData;

            public uint BoundInstanceId;
            private uint LinkedInstanceID;
            private uint _padding1;
            private uint _padding2;

            public static EventInstanceObject Read( BinaryReader br )
            {
                EventInstanceObject ret = new EventInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = GameInstanceObject.Read( br );
                ret.BoundInstanceId = br.ReadUInt32();
                ret.LinkedInstanceID = br.ReadUInt32();
                ret._padding1 = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct EnvLocationInstanceObject : IInstanceObject
        {
            public string SHAmbientLightAssetPath;
            public string EnvMapAssetPath;

            public static EnvLocationInstanceObject Read( BinaryReader br )
            {
                EnvLocationInstanceObject ret = new EnvLocationInstanceObject();
                long start = br.BaseStream.Position;

                ret.SHAmbientLightAssetPath = br.ReadStringOffset( start - 48 );
                ret.EnvMapAssetPath = br.ReadStringOffset( start - 48 );

                return ret;
            }
        }

        public struct EventRangeInstanceObject : IInstanceObject
        {
            public TriggerBoxInstanceObject ParentData;
            //TODO: 12 more bytes here. Might be flags idk

            public static EventRangeInstanceObject Read( BinaryReader br )
            {
                EventRangeInstanceObject ret = new EventRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = TriggerBoxInstanceObject.Read( br );

                return ret;
            }
        }

        public struct QuestMarkerInstanceObject : IInstanceObject
        {
            public RangeType RangeType;
            private uint _padding1;
            private uint _padding2;

            public static QuestMarkerInstanceObject Read( BinaryReader br )
            {
                QuestMarkerInstanceObject ret = new QuestMarkerInstanceObject();
                long start = br.BaseStream.Position;

                ret.RangeType = (RangeType) br.ReadInt32();
                ret._padding1 = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct CollisionBoxInstanceObject : IInstanceObject
        {
            public TriggerBoxInstanceObject ParentData;

            public uint AttributeMask;
            public uint Attribute;
            public byte PushPlayerOut;
            private byte[] _padding01; //[3]
            public string CollisionAssetPath;
            private uint _padding2;


            public static CollisionBoxInstanceObject Read( BinaryReader br )
            {
                CollisionBoxInstanceObject ret = new CollisionBoxInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = TriggerBoxInstanceObject.Read( br );
                ret.AttributeMask = br.ReadUInt32();
                ret.Attribute = br.ReadUInt32();
                ret.PushPlayerOut = br.ReadByte();
                ret._padding01 = br.ReadBytes( 3 );
                ret.CollisionAssetPath = br.ReadStringOffset( start - 48 );
                ret._padding2 = br.ReadUInt32();
                
                return ret;
            }
        }

        // TODO: implement when encountered
        public struct DoorRangeInstanceObject : IInstanceObject// : TriggerBoxInstanceObject
        {
            private uint _padding2;
            private uint _padding3;
        }

        public struct LineVFXInstanceObject : IInstanceObject
        {
            public LineStyle LineStyle;
            private uint _padding1;
            private uint _padding2;

            public static LineVFXInstanceObject Read( BinaryReader br )
            {
                LineVFXInstanceObject ret = new LineVFXInstanceObject();
                long start = br.BaseStream.Position;

                ret.LineStyle = (LineStyle) br.ReadUInt32();
                ret._padding1 = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct ClientPathInstanceObject : IInstanceObject// : PathInstanceObject
        {
            public PathInstanceObject ParentData;

            public byte Ring;
            public byte[] Padding00; //[3]

            public static ClientPathInstanceObject Read( BinaryReader br )
            {
                ClientPathInstanceObject ret = new ClientPathInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = PathInstanceObject.Read( br );
                ret.Ring = br.ReadByte();
                ret.Padding00 = br.ReadBytes( 3 );

                return ret;
            }
        }

        public struct ServerPathInstanceObject : IInstanceObject
        {
            public PathInstanceObject ParentData;

            public static ServerPathInstanceObject Read( BinaryReader br )
            {
                ServerPathInstanceObject ret = new ServerPathInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = PathInstanceObject.Read( br );

                return ret;
            }
        }

        public struct GimmickRangeInstanceObject : IInstanceObject
        {
            public TriggerBoxInstanceObject ParentData;

            public GimmickType GimmickType;
            public uint GimmickKey;
            public byte RoomUseAttribute;
            public byte GroupId;
            public byte EnabledInDead;
            public byte Padding01;
            private uint _padding;

            public static GimmickRangeInstanceObject Read( BinaryReader br )
            {
                GimmickRangeInstanceObject ret = new GimmickRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = TriggerBoxInstanceObject.Read( br );
                ret.GimmickType = (GimmickType) br.ReadInt32();
                ret.GimmickKey = br.ReadUInt32();
                ret.RoomUseAttribute = br.ReadByte();
                ret.GroupId = br.ReadByte();
                ret.EnabledInDead = br.ReadByte();
                ret.Padding01 = br.ReadByte();
                ret._padding = br.ReadUInt32();
                
                return ret;
            }
        }

        public struct TargetMarkerInstanceObject : IInstanceObject
        {
            public float NamePlateOffsetY;
            public TargetMarkerType TargetMakerType;

            public static TargetMarkerInstanceObject Read( BinaryReader br )
            {
                TargetMarkerInstanceObject ret = new TargetMarkerInstanceObject();
                long start = br.BaseStream.Position;

                ret.NamePlateOffsetY = br.ReadSingle();
                ret.TargetMakerType = (TargetMarkerType) br.ReadInt32();

                return ret;
            }
        }

        public struct ChairMarkerInstanceObject : IInstanceObject
        {
            public byte LeftEnable;
            public byte RightEnable;
            public byte BackEnable;
            private byte _padding00;
            public ObjectType ObjectType;

            public static ChairMarkerInstanceObject Read( BinaryReader br )
            {
                ChairMarkerInstanceObject ret = new ChairMarkerInstanceObject();
                long start = br.BaseStream.Position;

                ret.LeftEnable = br.ReadByte();
                ret.RightEnable = br.ReadByte();
                ret.BackEnable = br.ReadByte();
                ret._padding00 = br.ReadByte();
                ret.ObjectType = (ObjectType) br.ReadInt32();

                return ret;
            }
        }

        public struct ClickableRangeInstanceObject : IInstanceObject// : TriggerBoxInstanceObject
        {
            private uint _padding2;

            public static ClickableRangeInstanceObject Read( BinaryReader br )
            {
                ClickableRangeInstanceObject ret = new ClickableRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct PrefetchRangeInstanceObject : IInstanceObject// : TriggerBoxInstanceObject
        {
            public TriggerBoxInstanceObject ParentData;

            public uint BoundInstanceId; // reference to the ExitRange the Prefetch is for
            private uint _padding2;

            public static PrefetchRangeInstanceObject Read( BinaryReader br )
            {
                PrefetchRangeInstanceObject ret = new PrefetchRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = TriggerBoxInstanceObject.Read( br );
                ret.BoundInstanceId = br.ReadUInt32();
                ret._padding2 = br.ReadUInt32();

                return ret;
            }
        }

        public struct FateRangeInstanceObject : IInstanceObject
        {
            public TriggerBoxInstanceObject ParentData;

            public uint FateLayoutLabelId;

            public static FateRangeInstanceObject Read( BinaryReader br )
            {
                FateRangeInstanceObject ret = new FateRangeInstanceObject();
                long start = br.BaseStream.Position;

                ret.ParentData = TriggerBoxInstanceObject.Read( br );
                ret.FateLayoutLabelId = br.ReadUInt32();

                return ret;
            }
        }

        public struct InstanceObject
        {
            public LayerEntryType AssetType;
            public uint InstanceId;
            public string Name;
            public Transformation Transform;
            public IInstanceObject Object;

            public static InstanceObject Read( BinaryReader br )
            {
                InstanceObject ret = new InstanceObject();
                long start = br.BaseStream.Position;

                ret.AssetType = (LayerEntryType)br.ReadInt32();
                ret.InstanceId = br.ReadUInt32();
                ret.Name = br.ReadStringOffset( start );

                ret.Transform = Transformation.Read( br );

                switch ( ret.AssetType )
                {
                    case LayerEntryType.BG: ret.Object = BGInstanceObject.Read( br ); break; //0x1
                    case LayerEntryType.LayLight: ret.Object = LightInstanceObject.Read( br ); break; //0x3
                    case LayerEntryType.VFX: ret.Object = VFXInstanceObject.Read( br ); break; //0x4
                    case LayerEntryType.PositionMarker: ret.Object = PositionMarkerInstanceObject.Read( br ); break; //0x5
                    case LayerEntryType.SharedGroup: ret.Object = SharedGroupInstanceObject.Read( br ); break; //0x6
                    case LayerEntryType.Sound: ret.Object = SoundInstanceObject.Read( br ); break; //0x7
                    case LayerEntryType.EventNPC: ret.Object = ENPCInstanceObject.Read( br ); break; //0x8
                    case LayerEntryType.BattleNPC: ret.Object = BNPCInstanceObject.Read( br ); break; //0x9
                    case LayerEntryType.Aetheryte: ret.Object = AetheryteInstanceObject.Read( br ); break; //0xC
                    case LayerEntryType.EnvSet: ret.Object = EnvSetInstanceObject.Read( br ); break; //0xD
                    case LayerEntryType.Gathering: ret.Object = GatheringInstanceObject.Read( br ); break; //0x#
                    case LayerEntryType.Treasure: ret.Object = TreasureInstanceObject.Read( br ); break; //0x10
                    case LayerEntryType.PopRange: ret.Object = PopRangeInstanceObject.Read( br ); break; //0x28
                    case LayerEntryType.ExitRange: ret.Object = ExitRangeInstanceObject.Read( br ); break; //0x29
                    case LayerEntryType.MapRange: ret.Object = MapRangeInstanceObject.Read( br ); break; //0x2B
                    case LayerEntryType.EventObject: ret.Object = EventInstanceObject.Read( br ); break; //0x2D
                    case LayerEntryType.EnvLocation: ret.Object = EnvLocationInstanceObject.Read( br ); break; //0x2F
                    case LayerEntryType.EventRange: ret.Object = EventRangeInstanceObject.Read( br ); break; //0x31
                    case LayerEntryType.QuestMarker: ret.Object = QuestMarkerInstanceObject.Read( br ); break; //0x33
                    case LayerEntryType.CollisionBox: ret.Object = CollisionBoxInstanceObject.Read( br ); break; //0x39
                    case LayerEntryType.LineVFX: ret.Object = LineVFXInstanceObject.Read( br ); break; //0x3B
                    case LayerEntryType.ClientPath: ret.Object = ClientPathInstanceObject.Read( br ); break; //0x41
                    case LayerEntryType.ServerPath: ret.Object = ServerPathInstanceObject.Read( br ); break; //0x42
                    case LayerEntryType.GimmickRange: ret.Object = GimmickRangeInstanceObject.Read( br ); break; //0x43
                    case LayerEntryType.TargetMarker: ret.Object = TargetMarkerInstanceObject.Read( br ); break; //0x44
                    case LayerEntryType.ChairMarker: ret.Object = ChairMarkerInstanceObject.Read( br ); break; //0x45
                    case LayerEntryType.PrefetchRange: ret.Object = PrefetchRangeInstanceObject.Read( br ); break; //0x47
                    case LayerEntryType.FateRange: ret.Object = FateRangeInstanceObject.Read( br ); break; //0x48
                    default:
                        Debug.WriteLine($"Unknown asset {ret.AssetType.ToString()} @ {br.BaseStream.Position}.");
                        break;
                }

//                Debug.WriteLine($"Read {ret.Object.GetType().FullName}");

                return ret;
            }
        }

        public struct LayerSetReferenced
        {
            public uint LayerSetId;

            public static LayerSetReferenced Read( BinaryReader br )
            {
                LayerSetReferenced ret = new LayerSetReferenced();
                long start = br.BaseStream.Position;

                ret.LayerSetId = br.ReadUInt32();

                return ret;
            }
        }

        public struct LayerSetReferencedList
        {
            public LayerSetReferencedType ReferencedType;
            public int LayerSets;
            public int LayerSetCount;

            public static LayerSetReferencedList Read( BinaryReader br )
            {
                LayerSetReferencedList ret = new LayerSetReferencedList();
                long start = br.BaseStream.Position;

                ret.ReferencedType = (LayerSetReferencedType) br.ReadInt32();
                ret.LayerSets = br.ReadInt32();
                ret.LayerSetCount = br.ReadInt32();

                return ret;
            }
        }

        public struct OBSetReferenced
        {
            public LayerEntryType AssetType;
            public uint InstanceId;
            public string OBSetAssetPath;

            public static OBSetReferenced Read( BinaryReader br )
            {
                OBSetReferenced ret = new OBSetReferenced();
                long start = br.BaseStream.Position;

                ret.AssetType = (LayerEntryType) br.ReadInt32();
                ret.InstanceId = br.ReadUInt32();
                ret.OBSetAssetPath = br.ReadStringOffset( start );

                return ret;
            }
        }

        public struct OBSetEnableReferenced
        {
            public LayerEntryType AssetType;
            public uint InstanceId;
            public byte OBSetEnable;
            public byte OBSetEmissiveEnable;
            private byte[] _padding00; //[2]

            public static OBSetEnableReferenced Read( BinaryReader br )
            {
                OBSetEnableReferenced ret = new OBSetEnableReferenced();
                long start = br.BaseStream.Position;

                ret.AssetType = (LayerEntryType) br.ReadInt32();
                ret.InstanceId = br.ReadUInt32();
                ret.OBSetEnable = br.ReadByte();
                ret.OBSetEmissiveEnable = br.ReadByte();
                ret._padding00 = br.ReadBytes( 2 );

                return ret;
            }
        }

        public struct Layer
        {
            public uint LayerId;
            public string Name;
            public int _InstanceObjects;
            public int InstanceObjectCount;
            public byte ToolModeVisible;
            public byte ToolModeReadOnly;
            public byte IsBushLayer;
            public byte PS3Visible;
            public int _LayerSetReferencedList;
            public ushort FestivalID;
            public ushort FestivalPhaseID;
            public byte IsTemporary;
            public byte IsHousing;
            public ushort VersionMask;
            private uint _padding;
            public int ObSetReferencedList;
            public int ObSetReferencedListCount;
            public int ObSetEnableReferencedList;
            public int ObSetEnableReferencedListCount;
            public LayerSetReferencedList LayerSetReferencedList;

            public InstanceObject[] InstanceObjects;
            public LayerSetReferenced[] LayerSetReferences;
            public OBSetReferenced[] OBSetReferencedList;
            public OBSetEnableReferenced[] OBSetEnableReferencedList;

            public static Layer Read( BinaryReader br )
            {
                Layer ret = new Layer();
                long start = br.BaseStream.Position;

                ret.LayerId = br.ReadUInt32();
                ret.Name = br.ReadStringOffset( start );
                ret._InstanceObjects = br.ReadInt32();
                ret.InstanceObjectCount = br.ReadInt32();
                ret.ToolModeVisible = br.ReadByte();
                ret.ToolModeReadOnly = br.ReadByte();
                ret.IsBushLayer = br.ReadByte();
                ret.PS3Visible = br.ReadByte();
                ret._LayerSetReferencedList = br.ReadInt32();
                ret.FestivalID = br.ReadUInt16();
                ret.FestivalPhaseID = br.ReadUInt16();
                ret.IsTemporary = br.ReadByte();
                ret.IsHousing = br.ReadByte();
                ret.VersionMask = br.ReadUInt16();
                ret._padding = br.ReadUInt32();
                ret.ObSetReferencedList = br.ReadInt32();
                ret.ObSetReferencedListCount = br.ReadInt32();
                ret.ObSetEnableReferencedList = br.ReadInt32();
                ret.ObSetEnableReferencedListCount = br.ReadInt32();

                br.BaseStream.Position = start + ret._InstanceObjects;
                var instanceOffsets = br.ReadStructures< Int32 >( ret.InstanceObjectCount );

                br.BaseStream.Position = start + ret._LayerSetReferencedList;
                ret.LayerSetReferencedList = LayerSetReferencedList.Read( br );

                ret.InstanceObjects = new InstanceObject[ret.InstanceObjectCount];
                ret.LayerSetReferences = new LayerSetReferenced[ret.LayerSetReferencedList.LayerSetCount];
                ret.OBSetReferencedList = new OBSetReferenced[ret.ObSetReferencedListCount];
                ret.OBSetEnableReferencedList = new OBSetEnableReferenced[ret.ObSetEnableReferencedListCount];
                
                for( int i = 0; i < ret.InstanceObjectCount; i++ )
                {
                    br.BaseStream.Position = start + ret._InstanceObjects + instanceOffsets[i];
                    ret.InstanceObjects[i] = InstanceObject.Read( br );
                }

                br.BaseStream.Position = start + ret.LayerSetReferencedList.LayerSets;
                for (int i = 0; i < ret.LayerSetReferencedList.LayerSetCount; i++)
                    ret.LayerSetReferences[i] = LayerSetReferenced.Read( br );

                br.BaseStream.Position = start + ret.ObSetReferencedList;
                for (int i = 0; i < ret.ObSetReferencedListCount; i++)
                    ret.OBSetReferencedList[i] = OBSetReferenced.Read( br );

                br.BaseStream.Position = start + ret.ObSetEnableReferencedList;
                for (int i = 0; i < ret.ObSetEnableReferencedListCount; i++)
                    ret.OBSetEnableReferencedList[i] = OBSetEnableReferenced.Read( br );

                return ret;
            }
        }
    }
}
