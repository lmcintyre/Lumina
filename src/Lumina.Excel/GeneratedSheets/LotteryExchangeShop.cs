// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "LotteryExchangeShop", columnHash: 0xfd7b4ce5 )]
    public class LotteryExchangeShop : ExcelRow
    {
        
        public SeString Unknown0 { get; set; }
        public LazyRow< Item >[] ItemAccepted { get; set; }
        public uint[] AmountAccepted { get; set; }
        public byte[] Unknown540 { get; set; }
        public byte[] Unknown541 { get; set; }
        public SeString Lua { get; set; }
        public LazyRow< LogMessage >[] LogMessage { get; set; }
        public bool Unknown133 { get; set; }
        
        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Unknown0 = parser.ReadColumn< SeString >( 0 );
            ItemAccepted = new LazyRow< Item >[ 32 ];
            for( var i = 0; i < 32; i++ )
                ItemAccepted[ i ] = new LazyRow< Item >( gameData, parser.ReadColumn< int >( 1 + i ), language );
            AmountAccepted = new uint[ 32 ];
            for( var i = 0; i < 32; i++ )
                AmountAccepted[ i ] = parser.ReadColumn< uint >( 33 + i );
            Unknown540 = new byte[ 32 ];
            for( var i = 0; i < 32; i++ )
                Unknown540[ i ] = parser.ReadColumn< byte >( 65 + i );
            Unknown541 = new byte[ 32 ];
            for( var i = 0; i < 32; i++ )
                Unknown541[ i ] = parser.ReadColumn< byte >( 97 + i );
            Lua = parser.ReadColumn< SeString >( 129 );
            LogMessage = new LazyRow< LogMessage >[ 3 ];
            for( var i = 0; i < 3; i++ )
                LogMessage[ i ] = new LazyRow< LogMessage >( gameData, parser.ReadColumn< uint >( 130 + i ), language );
            Unknown133 = parser.ReadColumn< bool >( 133 );
        }
    }
}