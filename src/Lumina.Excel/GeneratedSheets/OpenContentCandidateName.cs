// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "OpenContentCandidateName", columnHash: 0xdebb20e3 )]
    public class OpenContentCandidateName : ExcelRow
    {
        
        public SeString Name;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Name = parser.ReadColumn< SeString >( 0 );
        }
    }
}