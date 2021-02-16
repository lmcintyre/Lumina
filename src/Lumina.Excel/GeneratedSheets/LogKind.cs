// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "LogKind", columnHash: 0x23b962ed )]
    public class LogKind : ExcelRow
    {
        
        public byte Unknown0;
        public SeString Format;
        public bool Unknown2;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Unknown0 = parser.ReadColumn< byte >( 0 );
            Format = parser.ReadColumn< SeString >( 1 );
            Unknown2 = parser.ReadColumn< bool >( 2 );
        }
    }
}