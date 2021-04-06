using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Lumina.Data.Structs;
using Lumina.Extensions;

namespace Lumina.Data
{
    public class SqPackIndex : SqPack
    {
        public bool IsIndex2 { get; set; }

        public SqPackIndexHeader IndexHeader { get; private set; }

        public Dictionary< ulong, IndexHashTableEntry > HashTableEntries { get; set; } = null!;
        public Dictionary< uint, Index2HashTableEntry > HashTableEntries2 { get; set; } = null!;

        internal SqPackIndex( FileInfo indexFile, GameData gameData ) : base( indexFile, gameData )
        {
            IsIndex2 = indexFile.Extension == ".index2";

            if( IsIndex2 )
            {
                LoadIndex2();
            }
            else
            {
                LoadIndex();
            }
        }

        private void LoadIndex()
        {
            using var fs = File.OpenRead();
            using var br = new BinaryReader( fs );

            // skip og header
            fs.Position = SqPackHeader.size;

            // read index hdr
            IndexHeader = br.ReadStructure< SqPackIndexHeader >();

            // read hashtable entries
            fs.Position = IndexHeader.index_data_offset;
            var entryCount = IndexHeader.index_data_size / Marshal.SizeOf( typeof( IndexHashTableEntry ) );

            HashTableEntries = br
                .ReadStructures< IndexHashTableEntry >( (int)entryCount )
                .ToDictionary( k => k.hash, v => v );
        }

        private void LoadIndex2()
        {
            using var fs = File.OpenRead();
            using var br = new BinaryReader( fs );

            // skip og header
            fs.Position = SqPackHeader.size;

            // read index hdr
            IndexHeader = br.ReadStructure< SqPackIndexHeader >();

            // read hashtable entries
            fs.Position = IndexHeader.index_data_offset;
            var entryCount = IndexHeader.index_data_size / Marshal.SizeOf( typeof( Index2HashTableEntry ) );

            HashTableEntries2 = br
                .ReadStructures< Index2HashTableEntry >( (int)entryCount )
                .ToDictionary( k => k.hash, v => v );
        }
    }
}