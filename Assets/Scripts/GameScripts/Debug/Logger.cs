using UnityEngine;
using System.Collections;

/// <summary>
/// Class to log events for debug purposes. Should output pretty HTML
/// </summary>
public class Logger  {
	
	private static Logger _logger;
	private static Logger logger {
		get {
			if (_logger==null)
				_logger = new Logger();
			return _logger;
		}
	}
	/// <summary>
	/// Log the specified message.
	/// </summary>
	/// <param name='message'>
	/// The Message.
	/// </param>
	public static void log(string message) {
		logger.log(message);
	}
	
	private Logger() {
	}
	
	private void log(string message) {
		
	}
	
}
