// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "InclusionShopCategory", columnHash: 0x3b24d05f )]
    public class InclusionShopCategory : ExcelRow
    {
        
        public SeString Name;
        public LazyRow< ClassJobCategory > ClassJobCategory;
        public LazyRow< InclusionShopSeries > InclusionShopSeries;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Name = parser.ReadColumn< SeString >( 0 );
            ClassJobCategory = new LazyRow< ClassJobCategory >( gameData, parser.ReadColumn< byte >( 1 ), language );
            InclusionShopSeries = new LazyRow< InclusionShopSeries >( gameData, parser.ReadColumn< ushort >( 2 ), language );
        }
    }
}