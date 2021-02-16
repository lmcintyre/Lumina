// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "DescriptionSection", columnHash: 0x2020acf6 )]
    public class DescriptionSection : ExcelRow
    {
        
        public LazyRow< DescriptionString > String;
        public LazyRow< DescriptionPage > Page;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            String = new LazyRow< DescriptionString >( gameData, parser.ReadColumn< ushort >( 0 ), language );
            Page = new LazyRow< DescriptionPage >( gameData, parser.ReadColumn< ushort >( 1 ), language );
        }
    }
}