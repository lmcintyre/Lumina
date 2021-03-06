// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "MasterpieceSupplyDuty", columnHash: 0x9f2c0d7f )]
    public class MasterpieceSupplyDuty : ExcelRow
    {
        public struct UnkStruct3Struct
        {
            public uint RequiredItem;
            public byte Quantity;
            public bool RequestHq;
            public ushort CollectabilityHighBonus;
            public ushort CollectabilityBonus;
            public ushort CollectabilityBase;
            public ushort ExpModifier;
            public ushort RewardScrips;
            public byte BonusMultiplier;
            public ushort ClassJobLevelMax;
            public byte Stars;
        }
        
        public LazyRow< ClassJob > ClassJob { get; set; }
        public byte ClassJobLevel { get; set; }
        public ushort RewardCurrency { get; set; }
        public UnkStruct3Struct[] UnkStruct3 { get; set; }
        
        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            ClassJob = new LazyRow< ClassJob >( gameData, parser.ReadColumn< byte >( 0 ), language );
            ClassJobLevel = parser.ReadColumn< byte >( 1 );
            RewardCurrency = parser.ReadColumn< ushort >( 2 );
            UnkStruct3 = new UnkStruct3Struct[ 8 ];
            for( var i = 0; i < 8; i++ )
            {
                UnkStruct3[ i ] = new UnkStruct3Struct();
                UnkStruct3[ i ].RequiredItem = parser.ReadColumn< uint >( 3 + ( i * 11 + 0 ) );
                UnkStruct3[ i ].Quantity = parser.ReadColumn< byte >( 3 + ( i * 11 + 1 ) );
                UnkStruct3[ i ].RequestHq = parser.ReadColumn< bool >( 3 + ( i * 11 + 2 ) );
                UnkStruct3[ i ].CollectabilityHighBonus = parser.ReadColumn< ushort >( 3 + ( i * 11 + 3 ) );
                UnkStruct3[ i ].CollectabilityBonus = parser.ReadColumn< ushort >( 3 + ( i * 11 + 4 ) );
                UnkStruct3[ i ].CollectabilityBase = parser.ReadColumn< ushort >( 3 + ( i * 11 + 5 ) );
                UnkStruct3[ i ].ExpModifier = parser.ReadColumn< ushort >( 3 + ( i * 11 + 6 ) );
                UnkStruct3[ i ].RewardScrips = parser.ReadColumn< ushort >( 3 + ( i * 11 + 7 ) );
                UnkStruct3[ i ].BonusMultiplier = parser.ReadColumn< byte >( 3 + ( i * 11 + 8 ) );
                UnkStruct3[ i ].ClassJobLevelMax = parser.ReadColumn< ushort >( 3 + ( i * 11 + 9 ) );
                UnkStruct3[ i ].Stars = parser.ReadColumn< byte >( 3 + ( i * 11 + 10 ) );
            }
        }
    }
}