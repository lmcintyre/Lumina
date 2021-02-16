// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "RelicNoteCategory", columnHash: 0xf8c2977f )]
    public class RelicNoteCategory : ExcelRow
    {
        
        public sbyte Unknown0;
        public SeString Text;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Unknown0 = parser.ReadColumn< sbyte >( 0 );
            Text = parser.ReadColumn< SeString >( 1 );
        }
    }
}