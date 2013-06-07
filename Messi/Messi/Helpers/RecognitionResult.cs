using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace GlobalEnglish.Recognition.DataContracts
{
    /// <summary>
    /// Indicates which kind of recognition activity will be performed.
    /// </summary>
    [Serializable]
    public enum RecognitionKind
    {
        /// <summary>
        /// Indicates the client is preparing equipment for recognition.
        /// </summary>
        ClientPreparation,

        /// <summary>
        /// Indicates the client is conducting a communication exercise.
        /// </summary>
        CommunicationPractice,

        /// <summary>
        /// Indicates the client is conducting a pronunciation exercise.
        /// </summary>
        PronunciationPractice

    } // RecognitionKind

    /// <summary>
    /// Indicates a kind of recognition result.
    /// </summary>
    [Serializable]
    public enum ResultKind
    {
        /// <summary>
        /// Indicates a server was unavailable.
        /// </summary>
        RecognitionUnavailable,

        /// <summary>
        /// Indicates a server was available.
        /// </summary>
        RecognitionAvailable,

        /// <summary>
        /// Indicates a recognition problem occurred.
        /// </summary>
        RecognitionError,

        /// <summary>
        /// Indicates a recognition failed.
        /// </summary>
        RecognitionFailed,

        /// <summary>
        /// Indicates a recognition succeeded.
        /// </summary>
        RecognitionSucceeded,

    } // ResultKind

    /// <summary>
    /// Indicates a kind of problem detected.
    /// </summary>
    public enum ResultDetailKind
    {
        /// <summary>
        /// Indicates an audio quality problem may exist.
        /// </summary>
        AudioQualityWarning,

        /// <summary>
        /// Indicates the audio file was missing.
        /// </summary>
        AudioFileMissing,

        /// <summary>
        /// Indicates the supplied grammar was rejected.
        /// </summary>
        GrammarWasRejected,

        /// <summary>
        /// Indicates the confidence was too low.
        /// </summary>
        ConfidenceWasTooLow,

        /// <summary>
        /// Indicates that the audio did not match the supplied grammar.
        /// </summary>
        NoRecognition,

        /// <summary>
        /// Indicates that the request was cancelled (interrupted) by the client.
        /// </summary>
        RecognitionCancelled,

    } // ResultDetailKind

    /// <summary>
    /// Indicates a kind of pronunciation problem.
    /// </summary>
    public enum PronunciationProblemKind
    {
        /// <summary>
        /// No problem was detected.
        /// </summary>
        None,

        /// <summary>
        /// A grapheme / phoneme pronunciation problem was detected.
        /// </summary>
        Grapheme,

        /// <summary>
        /// A word pronunciation problem was detected.
        /// </summary>
        Word

    } // PronunciationProblemKind

    /// <summary>
    /// Indicates the quality of the audio level.
    /// </summary>
    public enum AudioLevel
    {
        /// <summary>
        /// Indicates audio level is normal.
        /// </summary>
        Normal,

        /// <summary>
        /// Indicates audio level is too low.
        /// </summary>
        TooLow,

        /// <summary>
        /// Indicates audio level is too high.
        /// </summary>
        TooHigh

    } // AudioLevel

    /// <summary>
    /// Indicates whether audio truncation occurred.
    /// </summary>
    public enum AudioTruncation
    {
        /// <summary>
        /// Indicates no truncation occurred.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the audio was truncated at the start.
        /// </summary>
        Start,

        /// <summary>
        /// Indicates that the audio was truncated at the end.
        /// </summary>
        End,

        /// <summary>
        /// Indicates that the audio was truncated at both start and end.
        /// </summary>
        Both

    } // AudioTruncation

    /// <summary>
    /// Indicates whether noise was detected in the audio.
    /// </summary>
    public enum AudioNoise
    {
        /// <summary>
        /// Indicates no noise was detected.
        /// </summary>
        None,

        /// <summary>
        /// Indicates the audio had a low noise level.
        /// </summary>
        Low,

        /// <summary>
        /// Indicates the audio had a moderate noise level.
        /// </summary>
        Moderate,

        /// <summary>
        /// Indicates the audio had a high noise level.
        /// </summary>
        High

    } // 

    /// <summary>
    /// Describes the result of a speech recognition.
    /// </summary>
    /// <remarks>
    /// <h4>Responsibilities:</h4>
    /// <list type="bullet">
    /// <item>indicates whether a recognition succeeded</item>
    /// <item>knows the results of a successful recognition</item>
    /// </list>
    /// ASR Phoneme Tracking (Wiki)
    /// GetFloatParameter(ep.EndSeconds)??
    /// </remarks>
    [Serializable]
    public class RecognitionResult
    {
		public string RecognizedText;

        /// <summary>
        /// Indicates the kind of result.
        /// </summary>
		public ResultKind TypeKind;

		[XmlAttribute]
		public string Type;

        /// <summary>
        /// Indicates a result detail.
        /// </summary>
		public ResultDetailKind[] ResultDetailKinds;

        
        /// <summary>
        /// A recognition message.
        /// </summary>

		public string Message;

        /// <summary>
        /// The amount of time (msecs) spent during recognition.
        /// </summary>

		public int RecognitionTime;

        /// <summary>
        /// The amount of time (msecs) spent during recognition, 
        /// including time spent waiting in queue.
        /// </summary>

		public int QueuedRecognitionTime;

        /// <summary>
        /// The saved audio file name.
        /// </summary>

		public string RecordedFileName;

        /// <summary>
        /// The saved audio file type.
        /// </summary>

		public string RecordedFileType;

        /// <summary>
        /// The audio quality measures.
        /// </summary>

		public AudioQuality AudioMeasure;

        /// <summary>
        /// A matched sentence result (if recognized).
        /// </summary>

		[XmlElement]
		public SentenceMatch Sentence;

        /// <summary>
        /// Constructs a new RecognitionResult.
        /// </summary>
        public RecognitionResult()
        {
            TypeKind = ResultKind.RecognitionUnavailable;
            ResultDetailKinds = new ResultDetailKind[0];
            AudioMeasure = new AudioQuality();
            Message = string.Empty;
        }
    } // RecognitionResult

    /// <summary>
    /// Contains audio quality measures.
    /// </summary>
    [Serializable]
    
    public class AudioQuality
    {
        /// <summary>
        /// Indicates the level of the captured audio.
        /// </summary>
		public AudioLevel LevelKind;


		public string Level;

        /// <summary>
        /// Indicates whether the audio was truncated.
        /// </summary>
		public AudioTruncation TruncationKind;


		public string Truncation;

        /// <summary>
        /// The signal to noise ratio in the recognized audio.
        /// </summary>

		public float SignalNoiseRatio;

        /// <summary>
        /// Indicates the audio noise level.
        /// </summary>
		public AudioNoise NoiseLevelKind;


		public string NoiseLevel;

        /// <summary>
        /// The contents of RecClient result.AudioCheckString.
        /// </summary>

		public string Miscellany;

		public float NoiseAcceptanceThreshold;

        /// <summary>
        /// Constructs a new AudioQuality.
        /// </summary>
        public AudioQuality()
        {
            LevelKind = AudioLevel.Normal;
            NoiseLevelKind = AudioNoise.None;
            TruncationKind = AudioTruncation.None;
            NoiseAcceptanceThreshold = 30;
            SignalNoiseRatio = 0;
            Miscellany = string.Empty;
        }
    } // AudioQuality

    /// <summary>
    /// Contains matched sentence information.
    /// </summary>
    [Serializable]
    
    public class SentenceMatch
    {
        /// <summary>
        /// The recognized sentence text.
        /// </summary>

		[XmlAttribute]
		public string RecognizedText;

        /// <summary>
        /// The recognized sentence interpretation (if available).
        /// </summary>

		public string Interpretation;

        /// <summary>
        /// The matched answer index.
        /// </summary>

		[XmlAttribute]
		public int MatchedIndex;

        /// <summary>
        /// The sentence quality measures.
        /// </summary>

		[XmlElement]
		public SentenceQuality Quality;

        /// <summary>
        /// Any pronunciation problems that were detected.
        /// </summary>

		[XmlElement("PronunciationProblem", typeof(PronunciationProblem))]
		public PronunciationProblem[] Problems;

        /// <summary>
        /// Constructs a new SentenceMatch.
        /// </summary>
        public SentenceMatch()
        {
            MatchedIndex = -1;
            RecognizedText = string.Empty;
            Interpretation = string.Empty;
            Problems = new PronunciationProblem[0];
            Quality = new SentenceQuality();
        }
    } // SentenceMatch

    /// <summary>
    /// Contains the recognized sentence quality measures.
    /// </summary>
    [Serializable]
    
    public class SentenceQuality
    {
        /// <summary>
        /// The number of frames contained in the recognized sentence.
        /// </summary>

		public int FrameCount;

        /// <summary>
        /// The recognized phrase confidence.
        /// </summary>

		public int PhraseConfidence;

        /// <summary>
        /// The confidence level of the recognized sentence.
        /// </summary>

		public int Confidence;

        /// <summary>
        /// The recognized sentence score.
        /// </summary>

		public int Score;

        /// <summary>
        /// The recognized word measures.
        /// </summary>

		public WordQuality[] Words;

        /// <summary>
        /// The recognized phoneme measures.
        /// </summary>

		public PhonemeQuality[] Phonemes;

        /// <summary>
        /// The configured phoneme acceptence threshold.
        /// </summary>

		public int PhonemeAcceptanceThreshold;

        /// <summary>
        /// The configured phoneme experience threshold.
        /// </summary>

		public float PhonemeExperienceThreshold;

        /// <summary>
        /// The configured phrase acceptance threshold.
        /// </summary>

		public int PhraseAcceptanceThreshold;

        /// <summary>
        /// The configured sentence acceptance threshold.
        /// </summary>

		public int SentenceAcceptanceThreshold;

        /// <summary>
        /// Constructs a new SentenceQuality.
        /// </summary>
        public SentenceQuality()
        {
            Phonemes = new PhonemeQuality[0];
            Words = new WordQuality[0];
            PhraseConfidence = 0;
            FrameCount = 0;
            Confidence = 0;
            Score = 0;
        }

        /// <summary>
        /// Indicates whether a recognition has sufficient confidence.
        /// </summary>
        public bool RecognitionAccepted
        {
            get
            {
                return (Confidence >= SentenceAcceptanceThreshold &&
                        PhraseConfidence >= PhraseAcceptanceThreshold);
            }
        }

        /// <summary>
        /// Indicates whether a phoneme was pronounced with acceptable quality.
        /// </summary>
        /// <param name="phoneme">a phoneme</param>
        /// <returns>whether a phoneme was accepted</returns>
        public bool Accepts(PhonemeQuality phoneme)
        {
            if (phoneme.Average == 0) return true;

            // NOTE: acceptance does not include threshold itself for phonemes
            return (phoneme.Score > PhonemeAcceptanceThreshold ||
                    phoneme.Average > PhonemeExperienceThreshold);
        }

    } // SentenceQuality

    /// <summary>
    /// Contains the quality measures for a recognized word.
    /// </summary>
    [Serializable]
    
    public class WordQuality
    {
        /// <summary>
        /// The number of recognized phonemes.
        /// </summary>

		public int PhoneCount;

        /// <summary>
        /// The start frame of a word.
        /// </summary>

		public int StartFrame;

        /// <summary>
        /// The end frame of a word.
        /// </summary>

		public int EndFrame;

        /// <summary>
        /// The number of frames that cover a word.
        /// </summary>

		public int FrameCount;

        /// <summary>
        /// A word confidence.
        /// </summary>

		public int Confidence;

        /// <summary>
        /// A word score.
        /// </summary>

		public int Score;

        /// <summary>
        /// Indicates whether confidence was acceptable.
        /// </summary>

		public bool Accepted;
    } // WordQuality

    /// <summary>
    /// Contains the quality measures for a recognized phoneme.
    /// </summary>
    [Serializable]
    
    public class PhonemeQuality
    {
        /// <summary>
        /// A phoneme name.
        /// </summary>
		public string PhoneName;

        /// <summary>
        /// The associated grapheme.
        /// </summary>
		public string Grapheme;

        /// <summary>
        /// The average of the historical phoneme scores.
        /// </summary>
		public float Average;

        /// <summary>
        /// A phoneme score.
        /// </summary>
		public int Score;

    } // PhonemeQuality

    /// <summary>
    /// Indicates and locates a pronunciation problem relative to the start of a sentence.
    /// </summary>
    [Serializable]
    
    public class PronunciationProblem
    {
        /// <summary>
        /// Indicates which kind of problem this represents.
        /// </summary>
		public PronunciationProblemKind TypeKind;


		public string Type;

        /// <summary>
        /// A grapheme or word.
        /// </summary>

		public string Grapheme;

        /// <summary>
        /// A phoneme or empty.
        /// </summary>

		public string Phoneme;

        /// <summary>
        /// A character offset within a sentence.
        /// </summary>

		public int Offset;

        public PronunciationProblem()
        {
            TypeKind = PronunciationProblemKind.Word;
            Grapheme = string.Empty;
            Phoneme = string.Empty;
            Offset = 0;
        }

    } // PronunciationProblem
}
