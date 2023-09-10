using NAudio.Wave;
using System;
using System.Threading.Tasks;

class GenerateSound
{

    public GenerateSound()
    {
        PlayWindSoundAsync();
    }
    public async Task PlayWindSoundAsync()
    {
        await GenerateAndPlayWindSoundAsync();
    }

    async Task GenerateAndPlayWindSoundAsync()
    {
        int sampleRate = 44100;
        int durationMillis = 20000; // 20 Sekunden

        using (var waveOut = new WaveOutEvent())
        {
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 10);
            var sampleProvider = new WindSoundProvider(sampleRate, durationMillis);

            waveOut.Init(sampleProvider);
            waveOut.Play();

            Console.WriteLine($"Playing wind sound for {durationMillis} ms...");
            await Task.Delay(durationMillis);
        }
    }

    class WindSoundProvider : ISampleProvider
    {
        private int sampleRate;
        private int numSamples;
        private int position;

        public WaveFormat WaveFormat { get; }

        public WindSoundProvider(int sampleRate, int durationMillis)
        {
            this.sampleRate = sampleRate;
            this.numSamples = sampleRate * durationMillis / 1000;
            this.position = 0;
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                float noise = (float)(random.NextDouble() * 2 - 1); // Zufälliges Rauschen
                buffer[i + offset] = noise * (float)Math.Sin(.5 * Math.PI * position++ / sampleRate*1.75);

                if (position >= numSamples)
                {
                    position = 0;
                }
            }

            return count;
        }
    }
}
