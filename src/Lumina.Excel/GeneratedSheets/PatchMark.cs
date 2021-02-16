// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "PatchMark", columnHash: 0x4b87e076 )]
    public class PatchMark : ExcelRow
    {
        
        public sbyte Category;
        public byte SubCategoryType;
        public ushort SubCategory;
        public byte Unknown3;
        public uint Unknown4;
        public uint MarkID;
        public byte Version;
        public ushort Unknown7;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Category = parser.ReadColumn< sbyte >( 0 );
            SubCategoryType = parser.ReadColumn< byte >( 1 );
            SubCategory = parser.ReadColumn< ushort >( 2 );
            Unknown3 = parser.ReadColumn< byte >( 3 );
            Unknown4 = parser.ReadColumn< uint >( 4 );
            MarkID = parser.ReadColumn< uint >( 5 );
            Version = parser.ReadColumn< byte >( 6 );
            Unknown7 = parser.ReadColumn< ushort >( 7 );
        }
    }
}