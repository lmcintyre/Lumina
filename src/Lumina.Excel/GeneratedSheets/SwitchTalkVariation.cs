// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "SwitchTalkVariation", columnHash: 0x031e9614 )]
    public class SwitchTalkVariation : ExcelRow
    {
        
        public uint Quest0;
        public LazyRow< Quest > Quest1;
        public byte Unknown2;
        public LazyRow< DefaultTalk > DefaultTalk;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Quest0 = parser.ReadColumn< uint >( 0 );
            Quest1 = new LazyRow< Quest >( gameData, parser.ReadColumn< uint >( 1 ), language );
            Unknown2 = parser.ReadColumn< byte >( 2 );
            DefaultTalk = new LazyRow< DefaultTalk >( gameData, parser.ReadColumn< uint >( 3 ), language );
        }
    }
}