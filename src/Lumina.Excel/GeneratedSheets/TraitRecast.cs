// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "TraitRecast", columnHash: 0xdc23efe7 )]
    public class TraitRecast : ExcelRow
    {
        
        public LazyRow< Trait > Trait;
        public LazyRow< Action > Action;
        public ushort Timeds;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Trait = new LazyRow< Trait >( gameData, parser.ReadColumn< ushort >( 0 ), language );
            Action = new LazyRow< Action >( gameData, parser.ReadColumn< ushort >( 1 ), language );
            Timeds = parser.ReadColumn< ushort >( 2 );
        }
    }
}