using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Audio;
using System.IO; // Add this for MemoryStream and SeekOrigin
using System.Text; // Add this for Encoding



public static class AudioClipExtensions
{
    // Converts an AudioClip to a WAV byte array.
    public static byte[] ToWav(this AudioClip clip)
    {
        using (var memoryStream = new MemoryStream())
        {
            // Write WAV header to the stream
            memoryStream.Write(new byte[44], 0, 44); // placeholder for the header

            // Write AudioClip data to the stream
            var samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);
            Int16[] intData = new Int16[samples.Length];
            Byte[] bytesData = new Byte[samples.Length * 2];
            const float rescaleFactor = 32767; // to convert float to Int16

            for (int i = 0; i < samples.Length; i++)
            {
                intData[i] = (short)(samples[i] * rescaleFactor);
                Byte[] byteArr = new Byte[2];
                byteArr = BitConverter.GetBytes(intData[i]);
                byteArr.CopyTo(bytesData, i * 2);
            }

            memoryStream.Write(bytesData, 0, bytesData.Length);

            // At this point, WAV data is complete; write header
            WriteHeader(memoryStream, clip);

            return memoryStream.ToArray();
        }
    }

    private static void WriteHeader(MemoryStream stream, AudioClip clip)
    {
        int hz = clip.frequency;
        int channels = clip.channels;
        int samples = clip.samples;

        stream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = Encoding.UTF8.GetBytes("RIFF");
        stream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(stream.Length - 8);
        stream.Write(chunkSize, 0, 4);

        Byte[] wave = Encoding.UTF8.GetBytes("WAVE");
        stream.Write(wave, 0, 4);

        Byte[] fmt = Encoding.UTF8.GetBytes("fmt ");
        stream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        stream.Write(subChunk1, 0, 4);

        UInt16 two = 1;
        UInt16 one = (UInt16)channels;
        stream.Write(BitConverter.GetBytes(two), 0, 2);
        stream.Write(BitConverter.GetBytes(one), 0, 2);

        stream.Write(BitConverter.GetBytes(hz), 0, 4);
        stream.Write(BitConverter.GetBytes(hz * channels * 2), 0, 4);
        stream.Write(BitConverter.GetBytes((UInt16)(channels * 2)), 0, 2);
        stream.Write(BitConverter.GetBytes((UInt16)16), 0, 2);

        Byte[] dataString = Encoding.UTF8.GetBytes("data");
        stream.Write(dataString, 0, 4);

        stream.Write(BitConverter.GetBytes(samples * channels * 2), 0, 4);
    }
}
