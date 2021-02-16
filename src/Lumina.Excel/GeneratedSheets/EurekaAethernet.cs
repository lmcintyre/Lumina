// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "EurekaAethernet", columnHash: 0xd870e208 )]
    public class EurekaAethernet : ExcelRow
    {
        
        public LazyRow< PlaceName > Location;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Location = new LazyRow< PlaceName >( gameData, parser.ReadColumn< ushort >( 0 ), language );
        }
    }
}