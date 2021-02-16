// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "Description", columnHash: 0x1933868c )]
    public class Description : ExcelRow
    {
        
        public LazyRow< Quest > Quest;
        public SeString TextLong;
        public SeString TextShort;
        public SeString TextCommentary;
        public bool Unknown4;
        public LazyRow< DescriptionSection > Section;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Quest = new LazyRow< Quest >( gameData, parser.ReadColumn< uint >( 0 ), language );
            TextLong = parser.ReadColumn< SeString >( 1 );
            TextShort = parser.ReadColumn< SeString >( 2 );
            TextCommentary = parser.ReadColumn< SeString >( 3 );
            Unknown4 = parser.ReadColumn< bool >( 4 );
            Section = new LazyRow< DescriptionSection >( gameData, parser.ReadColumn< uint >( 5 ), language );
        }
    }
}