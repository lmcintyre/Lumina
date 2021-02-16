// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "FCRights", columnHash: 0xce73d687 )]
    public class FCRights : ExcelRow
    {
        
        public SeString Name;
        public SeString Description;
        public ushort Icon;
        public LazyRow< FCRank > FCRank;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Name = parser.ReadColumn< SeString >( 0 );
            Description = parser.ReadColumn< SeString >( 1 );
            Icon = parser.ReadColumn< ushort >( 2 );
            FCRank = new LazyRow< FCRank >( gameData, parser.ReadColumn< byte >( 3 ), language );
        }
    }
}