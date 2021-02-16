// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "BuddySkill", columnHash: 0xe3220ddc )]
    public class BuddySkill : ExcelRow
    {
        
        public byte BuddyLevel;
        public bool IsActive;
        public ushort Defender;
        public ushort Attacker;
        public ushort Healer;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            BuddyLevel = parser.ReadColumn< byte >( 0 );
            IsActive = parser.ReadColumn< bool >( 1 );
            Defender = parser.ReadColumn< ushort >( 2 );
            Attacker = parser.ReadColumn< ushort >( 3 );
            Healer = parser.ReadColumn< ushort >( 4 );
        }
    }
}