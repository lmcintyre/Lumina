// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "PartyContentTextData", columnHash: 0xdebb20e3 )]
    public class PartyContentTextData : ExcelRow
    {
        
        public SeString Data;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Data = parser.ReadColumn< SeString >( 0 );
        }
    }
}