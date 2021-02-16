// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "SpearfishingItem", columnHash: 0xd17632b4 )]
    public class SpearfishingItem : ExcelRow
    {
        
        public SeString Description;
        public LazyRow< Item > Item;
        public LazyRow< GatheringItemLevelConvertTable > GatheringItemLevel;
        public LazyRow< FishingRecordType > FishingRecordType;
        public LazyRow< TerritoryType > TerritoryType;
        public bool IsVisible;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Description = parser.ReadColumn< SeString >( 0 );
            Item = new LazyRow< Item >( gameData, parser.ReadColumn< int >( 1 ), language );
            GatheringItemLevel = new LazyRow< GatheringItemLevelConvertTable >( gameData, parser.ReadColumn< ushort >( 2 ), language );
            FishingRecordType = new LazyRow< FishingRecordType >( gameData, parser.ReadColumn< byte >( 3 ), language );
            TerritoryType = new LazyRow< TerritoryType >( gameData, parser.ReadColumn< ushort >( 4 ), language );
            IsVisible = parser.ReadColumn< bool >( 5 );
        }
    }
}