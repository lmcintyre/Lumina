// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "HousingMerchantPose", columnHash: 0x3d65a9f1 )]
    public class HousingMerchantPose : ExcelRow
    {
        
        public LazyRow< ActionTimeline > ActionTimeline;
        public SeString Pose;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            ActionTimeline = new LazyRow< ActionTimeline >( gameData, parser.ReadColumn< ushort >( 0 ), language );
            Pose = parser.ReadColumn< SeString >( 1 );
        }
    }
}