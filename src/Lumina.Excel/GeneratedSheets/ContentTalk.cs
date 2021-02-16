// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "ContentTalk", columnHash: 0x5eb59ccb )]
    public class ContentTalk : ExcelRow
    {
        
        public LazyRow< ContentTalkParam > ContentTalkParam;
        public SeString Text;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            ContentTalkParam = new LazyRow< ContentTalkParam >( gameData, parser.ReadColumn< byte >( 0 ), language );
            Text = parser.ReadColumn< SeString >( 1 );
        }
    }
}