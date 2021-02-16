// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "QuestRedoChapterUI", columnHash: 0x4d7d2656 )]
    public class QuestRedoChapterUI : ExcelRow
    {
        
        public LazyRow< Quest > Quest;
        public byte Unknown1;
        public LazyRow< QuestRedoChapterUICategory > Category;
        public byte Unknown3;
        public uint QuestRedoUISmall;
        public uint QuestRedoUILarge;
        public uint QuestRedoUIWide;
        public SeString ChapterName;
        public SeString ChapterPart;
        public SeString Transient;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Quest = new LazyRow< Quest >( gameData, parser.ReadColumn< uint >( 0 ), language );
            Unknown1 = parser.ReadColumn< byte >( 1 );
            Category = new LazyRow< QuestRedoChapterUICategory >( gameData, parser.ReadColumn< byte >( 2 ), language );
            Unknown3 = parser.ReadColumn< byte >( 3 );
            QuestRedoUISmall = parser.ReadColumn< uint >( 4 );
            QuestRedoUILarge = parser.ReadColumn< uint >( 5 );
            QuestRedoUIWide = parser.ReadColumn< uint >( 6 );
            ChapterName = parser.ReadColumn< SeString >( 7 );
            ChapterPart = parser.ReadColumn< SeString >( 8 );
            Transient = parser.ReadColumn< SeString >( 9 );
        }
    }
}