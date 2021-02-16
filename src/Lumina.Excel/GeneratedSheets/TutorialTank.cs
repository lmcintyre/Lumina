// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "TutorialTank", columnHash: 0xdcfd9eba )]
    public class TutorialTank : ExcelRow
    {
        
        public LazyRow< Tutorial > Objective;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Objective = new LazyRow< Tutorial >( gameData, parser.ReadColumn< byte >( 0 ), language );
        }
    }
}