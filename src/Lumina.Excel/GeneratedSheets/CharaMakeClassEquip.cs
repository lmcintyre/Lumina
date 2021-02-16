// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "CharaMakeClassEquip", columnHash: 0x41dafacb )]
    public class CharaMakeClassEquip : ExcelRow
    {
        
        public ulong Helmet;
        public ulong Top;
        public ulong Glove;
        public ulong Down;
        public ulong Shoes;
        public ulong Weapon;
        public ulong SubWeapon;
        public LazyRow< ClassJob > Class;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Helmet = parser.ReadColumn< ulong >( 0 );
            Top = parser.ReadColumn< ulong >( 1 );
            Glove = parser.ReadColumn< ulong >( 2 );
            Down = parser.ReadColumn< ulong >( 3 );
            Shoes = parser.ReadColumn< ulong >( 4 );
            Weapon = parser.ReadColumn< ulong >( 5 );
            SubWeapon = parser.ReadColumn< ulong >( 6 );
            Class = new LazyRow< ClassJob >( gameData, parser.ReadColumn< int >( 7 ), language );
        }
    }
}