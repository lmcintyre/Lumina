// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "InstanceContentBuff", columnHash: 0x2020acf6 )]
    public class InstanceContentBuff : ExcelRow
    {
        
        public ushort EchoStart;
        public ushort EchoDeath;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            EchoStart = parser.ReadColumn< ushort >( 0 );
            EchoDeath = parser.ReadColumn< ushort >( 1 );
        }
    }
}