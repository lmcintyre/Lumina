// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "FurnitureCatalogItemList", columnHash: 0x24e9963a )]
    public class FurnitureCatalogItemList : ExcelRow
    {
        
        public LazyRow< FurnitureCatalogCategory > Category;
        public LazyRow< Item > Item;
        public ushort Patch;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Category = new LazyRow< FurnitureCatalogCategory >( gameData, parser.ReadColumn< ushort >( 0 ), language );
            Item = new LazyRow< Item >( gameData, parser.ReadColumn< int >( 1 ), language );
            Patch = parser.ReadColumn< ushort >( 2 );
        }
    }
}