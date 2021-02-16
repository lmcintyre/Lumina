// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "ActionParam", columnHash: 0x8795cd75 )]
    public class ActionParam : ExcelRow
    {
        
        public short Name;
        public short Unknown1;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Name = parser.ReadColumn< short >( 0 );
            Unknown1 = parser.ReadColumn< short >( 1 );
        }
    }
}