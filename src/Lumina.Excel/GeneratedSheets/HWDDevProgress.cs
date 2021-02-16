// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "HWDDevProgress", columnHash: 0xcd4cb81c )]
    public class HWDDevProgress : ExcelRow
    {
        
        public bool CanGoNext;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            CanGoNext = parser.ReadColumn< bool >( 0 );
        }
    }
}