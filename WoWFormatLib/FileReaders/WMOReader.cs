﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WoWFormatLib.Structs.WMO;
using WoWFormatLib.Utils;

namespace WoWFormatLib.FileReaders
{
    public class WMOReader
    {
        public WMO wmofile;
        private string basedir;

        private List<String> m2Files;
        private List<String> wmoGroups;

        public WMOReader(string basedir)
        {
            this.basedir = basedir;
        }

        public void LoadWMO(string filename)
        {
            m2Files = new List<string>();
            wmoGroups = new List<string>();

            if (File.Exists(Path.Combine(basedir, filename)))
            {
                using (FileStream wmoStream = File.Open(Path.Combine(basedir, filename), FileMode.Open))
                {
                    ReadWMO(filename, wmoStream);
                }
            }
            else
            {
                new WoWFormatLib.Utils.MissingFile(filename);
            }
        }

        public void ReadMODNChunk(BlizzHeader chunk, BinaryReader bin)
        {
            //List of M2 filenames, but are still named after MDXs internally. Have to rename!
            var m2FilesChunk = bin.ReadBytes((int)chunk.Size);

            var str = new StringBuilder();

            for (var i = 0; i < m2FilesChunk.Length; i++)
            {
                if (m2FilesChunk[i] == '\0')
                {
                    if (str.Length > 1)
                    {
                        m2Files.Add(str.ToString());
                        var m2reader = new M2Reader(basedir);
                        m2reader.LoadM2(str.ToString());
                    }
                    str = new StringBuilder();
                }
                else
                {
                    str.Append((char)m2FilesChunk[i]);
                }
            }
        }

        public MOGI[] ReadMOGIChunk(BlizzHeader chunk, BinaryReader bin, uint num)
        {
            var groupInfo = new MOGI[num];
            for (var i = 0; i < num; i++)
            {
                groupInfo[i] = bin.Read<MOGI>();
            }
            return groupInfo;
        }

        public MOGN[] ReadMOGNChunk(BlizzHeader chunk, BinaryReader bin, uint num)
        {
            var wmoGroupsChunk = bin.ReadBytes((int)chunk.Size);
            var str = new StringBuilder();
            var nameList = new List<String>();
            for (var i = 0; i < wmoGroupsChunk.Length; i++)
            {
                if (wmoGroupsChunk[i] == '\0')
                {
                    if (str.Length > 1)
                    {
                        nameList.Add(str.ToString());
                    }
                    str = new StringBuilder();
                }
                else
                {
                    str.Append((char)wmoGroupsChunk[i]);
                }
            }

            if (nameList.Count != num)
            {
                throw new Exception("List of group names does not equal number of groups");
            }
            var groupNames = new MOGN[num];
            for (var i = 0; i < nameList.Count; i++)
            {
                Console.WriteLine(nameList[i]);
                groupNames[i].name = nameList[i];
            }
            return groupNames;
        }

        public MOGP ReadMOGPChunk(BlizzHeader chunk, BinaryReader bin)
        {
            bin.ReadBytes(68); //read rest of header
            MemoryStream stream = new MemoryStream(bin.ReadBytes((int)chunk.Size));
            var subbin = new BinaryReader(stream);
            BlizzHeader subchunk;
            long position = 0;
            MOGP mogp = new MOGP();
            while (position < stream.Length)
            {
                stream.Position = position;
                subchunk = new BlizzHeader(subbin.ReadChars(4), subbin.ReadUInt32());
                subchunk.Flip();
                position = stream.Position + subchunk.Size;
                Console.WriteLine(subchunk.ToString());
                switch (subchunk.ToString())
                {
                    case "MOVI": //Vertex indices for triangles
                        mogp.indices = ReadMOVIChunk(subchunk, subbin);
                        Console.WriteLine("Read " + mogp.indices.Length + " indices!");
                        break;

                    case "MOVT": //Vertices chunk
                        mogp.vertices = ReadMOVTChunk(subchunk, subbin);
                        break;

                    case "MOTV": //Texture coordinates
                        mogp.textureCoords = ReadMOTVChunk(subchunk, subbin);
                        break;

                    case "MOPY": //Material info for triangles, two bytes per triangle.
                    case "MONR": //Normals

                    case "MOBA": //Render batches
                    case "MOBS": //Unk
                    case "MODR": //Doodad references
                    case "MOBN": //Array of t_BSP_NODE
                    case "MOBR": //Face indices
                    case "MOLR": //Light references
                    case "MOCV": //Vertex colors
                    case "MDAL": //Unk (new in WoD?)
                    case "MLIQ": //Liquids
                    case "MOTA": //Unknown
                    case "MOPL": //Unknown
                        continue;
                    default:
                        throw new Exception(String.Format("Found unknown header at offset {1} \"{0}\" while we should've already read them all!", subchunk.ToString(), position.ToString()));
                }
            }
            return mogp;
        }

        public MOHD ReadMOHDChunk(BlizzHeader chunk, BinaryReader bin, string filename)
        {
            //Header for the map object. 64 bytes.
            // var MOHDChunk = bin.ReadBytes((int)chunk.Size);
            var header = new MOHD();
            header.nMaterials = bin.ReadUInt32();
            header.nGroups = bin.ReadUInt32();
            header.nPortals = bin.ReadUInt32();
            header.nLights = bin.ReadUInt32();
            header.nModels = bin.ReadUInt32();

            //Console.WriteLine("         " + nGroups.ToString() + " group(s)");

            return header;
        }

        public MOTX[] ReadMOTXChunk(BlizzHeader chunk, BinaryReader bin)
        {
            //List of BLP filenames
            var blpFilesChunk = bin.ReadBytes((int)chunk.Size);
            List<String> blpFiles = new List<string>();
            var str = new StringBuilder();

            for (var i = 0; i < blpFilesChunk.Length; i++)
            {
                if (blpFilesChunk[i] == '\0')
                {
                    if (str.Length > 1)
                    {
                        str.Replace("..", ".");
                        blpFiles.Add(str.ToString());
                        if (!System.IO.File.Exists(System.IO.Path.Combine(basedir, str.ToString())))
                        {
                            new WoWFormatLib.Utils.MissingFile(str.ToString());
                        }
                        else
                        {
                            // Console.WriteLine(str.ToString() + " exists!");
                            // Console.ReadLine();
                        }
                        //Console.WriteLine("         " + str.ToString());
                    }
                    str = new StringBuilder();
                }
                else
                {
                    str.Append((char)blpFilesChunk[i]);
                }
            }
            var textures = new MOTX[blpFiles.Count];
            for (var i = 0; i < blpFiles.Count; i++)
            {
                textures[i].filename = blpFiles[i];
            }
            return textures;
        }

        public MOVT[] ReadMOVTChunk(BlizzHeader chunk, BinaryReader bin)
        {
            var numVerts = chunk.Size / (sizeof(float) * 3);
            Console.WriteLine(numVerts + " vertices!");
            var vertices = new MOVT[numVerts];
            for (var i = 0; i < numVerts; i++)
            {
                vertices[i].vector = bin.Read<Vector3>();
            }
            return vertices;
        }

        private MOTV[] ReadMOTVChunk(BlizzHeader subchunk, BinaryReader subbin)
        {
            var numCoords = subchunk.Size / (sizeof(float) * 2);
            Console.WriteLine(numCoords + " texturecords!");
            var textureCoords = new MOTV[numCoords];
            for (var i = 0; i < numCoords; i++)
            {
                textureCoords[i].X = subbin.ReadSingle();
                textureCoords[i].Y = subbin.ReadSingle();
            }
            return textureCoords;
        }

        private MOVI[] ReadMOVIChunk(BlizzHeader chunk, BinaryReader bin)
        {
            var numIndices = chunk.Size / sizeof(ushort);
            Console.WriteLine(numIndices + " indices!");
            var indices = new MOVI[numIndices];
            for (var i = 0; i < numIndices; i++)
            {
                indices[i].indice = bin.ReadUInt16();
            }
            return indices;
        }

        private object ReadMOVVChunk(BlizzHeader chunk, BinaryReader bin)
        {
            throw new NotImplementedException();
        }

        private void ReadWMO(string filename, FileStream wmo)
        {
            var bin = new BinaryReader(wmo);
            BlizzHeader chunk;

            long position = 0;
            while (position < wmo.Length)
            {
                wmo.Position = position;
                chunk = new BlizzHeader(bin.ReadChars(4), bin.ReadUInt32());
                chunk.Flip();
                position = wmo.Position + chunk.Size;

                switch (chunk.ToString())
                {
                    case "MVER":
                        wmofile.version = bin.Read<MVER>();
                        if (wmofile.version.version != 17)
                        {
                            throw new Exception("Unsupported WMO version! (" + wmofile.version.version + ")");
                        }
                        continue;
                    case "MOTX":
                        wmofile.textures = ReadMOTXChunk(chunk, bin);
                        continue;
                    case "MOHD":
                        wmofile.header = ReadMOHDChunk(chunk, bin, filename);
                        continue;
                    case "MOGN":
                        wmofile.groupNames = ReadMOGNChunk(chunk, bin, wmofile.header.nGroups);
                        continue;
                    case "MOGI":
                        wmofile.groupInfo = ReadMOGIChunk(chunk, bin, wmofile.header.nGroups);
                        continue;
                    case "MOGP":
                    //ReadMOGPChunk(chunk, bin);
                    //continue;
                    case "MODN":
                    case "MOMT":
                    case "MOSB":
                    case "MOPV":
                    case "MOPT":
                    case "MOPR":
                    case "MOVV": //Visible block vertices
                    case "MOVB": //Visible block list
                    case "MOLT":
                    case "MODS":
                    case "MODD":
                    case "MFOG":
                    case "MCVP":
                        continue;
                    default:
                        throw new Exception(String.Format("{2} Found unknown header at offset {1} \"{0}\" while we should've already read them all!", chunk.ToString(), position.ToString(), filename));
                }
            }
            //open group files
            WMOGroupFile[] groupFiles = new WMOGroupFile[wmofile.header.nGroups];
            for (int i = 0; i < wmofile.header.nGroups; i++)
            {
                var groupfilename = filename.Replace(".WMO", "_" + i.ToString().PadLeft(3, '0') + ".WMO");
                groupfilename = filename.Replace(".wmo", "_" + i.ToString().PadLeft(3, '0') + ".wmo");
                if (!System.IO.File.Exists(System.IO.Path.Combine(basedir, groupfilename)))
                {
                    new WoWFormatLib.Utils.MissingFile(groupfilename);
                }
                else
                {
                    using (FileStream wmoStream = File.Open(Path.Combine(basedir, groupfilename), FileMode.Open))
                    {
                        groupFiles[i] = ReadWMOGroupFile(groupfilename, wmoStream);
                    }
                }
            }

            wmofile.group = groupFiles;
        }

        private WMOGroupFile ReadWMOGroupFile(string filename, FileStream wmo)
        {
            WMOGroupFile groupFile = new WMOGroupFile();

            var bin = new BinaryReader(wmo);
            BlizzHeader chunk;

            long position = 0;
            while (position < wmo.Length)
            {
                wmo.Position = position;
                chunk = new BlizzHeader(bin.ReadChars(4), bin.ReadUInt32());
                chunk.Flip();
                position = wmo.Position + chunk.Size;

                switch (chunk.ToString())
                {
                    case "MVER":
                        groupFile.version = bin.Read<MVER>();
                        if (wmofile.version.version != 17)
                        {
                            throw new Exception("Unsupported WMO version! (" + wmofile.version.version + ")");
                        }
                        continue;
                    case "MOGP":
                        groupFile.mogp = ReadMOGPChunk(chunk, bin);
                        continue;
                    default:
                        throw new Exception(String.Format("{2} Found unknown header at offset {1} \"{0}\" while we should've already read them all!", chunk.ToString(), position.ToString(), filename));
                }
            }
            return groupFile;
        }
    }
}