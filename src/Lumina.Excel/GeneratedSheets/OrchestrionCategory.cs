// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "OrchestrionCategory", columnHash: 0x7ac3ee00 )]
    public class OrchestrionCategory : ExcelRow
    {
        
        public SeString Name { get; set; }
        public byte HideOrder { get; set; }
        public uint Icon { get; set; }
        public byte Order { get; set; }
        
        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Name = parser.ReadColumn< SeString >( 0 );
            HideOrder = parser.ReadColumn< byte >( 1 );
            Icon = parser.ReadColumn< uint >( 2 );
            Order = parser.ReadColumn< byte >( 3 );
        }
    }
}