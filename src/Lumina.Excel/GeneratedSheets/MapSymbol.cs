// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "MapSymbol", columnHash: 0xe7e370e4 )]
    public class MapSymbol : ExcelRow
    {
        
        public int Icon;
        public LazyRow< PlaceName > PlaceName;
        public bool DisplayNavi;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Icon = parser.ReadColumn< int >( 0 );
            PlaceName = new LazyRow< PlaceName >( gameData, parser.ReadColumn< int >( 1 ), language );
            DisplayNavi = parser.ReadColumn< bool >( 2 );
        }
    }
}