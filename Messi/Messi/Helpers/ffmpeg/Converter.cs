using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Web;
using System.CodeDom.Compiler;
using MvcApplication1.Helpers.ffmpeg;

namespace GlobalEnglish.Step.Web.FileRepository.ffmpeg
{
	public class ConversionException : Exception
	{
		public ConversionException(String message)
			: base(message)
		{
		}
	}

	/// <summary>
	/// Converts audio data from some unknown format to PCM format.
	/// </summary>
	/// <remarks>
	/// <h4>Responsibilities:</h4>
	/// <list type="bullet">
	/// <item>converts audio data from some unknown format to PCM format</item>
	/// </list>
	/// </remarks>
	public class Converter
	{
		private static string _converterExecutable = "";
		private static TempFileCollection _tempFiles = new TempFileCollection();

		#region IFormatConversionService Members

		static Converter()
		{
			string tempFileName = Path.GetTempPath() + Path.GetRandomFileName() + ".exe";

			try
			{
				System.IO.File.WriteAllBytes(tempFileName, Resource1.ffmpeg);

				_converterExecutable = tempFileName;
				_tempFiles.AddFile(_converterExecutable, false);
			}
			catch (Exception e)
			{
				throw e;
				//Logger.WriteCriticalError("Unable to extract ffmpeg.exe from resource file to:" + tempFileName + ". Exception:" + e.Message);
			}
		}

		public void ConvertFile(string inputFilePath, string outputFilePath)
		{
			if (_converterExecutable == "")
			{
				throw new ApplicationException("ffmpeg is not initialized");
			}

			//run ffmpeg to convert input file to output file
			ProcessStartInfo procStart = new ProcessStartInfo();
			procStart.FileName = _converterExecutable;
			procStart.Arguments = string.Format("-i {0} -f flac {1}", inputFilePath, outputFilePath);
			procStart.ErrorDialog = false;
			procStart.UseShellExecute = false;
			procStart.RedirectStandardError = true;
			procStart.CreateNoWindow = true;

			using (Process decProc = Process.Start(procStart))
			{
				try
				{
					decProc.WaitForExit();
				}
				finally
				{
					if (decProc.ExitCode != 0)
					{
						throw new ConversionException(String.Format("Unable to convert audio file {0} to {1}. FFMPEG console output: {2}", inputFilePath, outputFilePath, decProc.StandardError.ReadToEnd()));
					}

					decProc.Close();
				}
			}
		}

		#endregion
	}
}
