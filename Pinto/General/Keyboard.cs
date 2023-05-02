// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// Authors:
//	Chris Toshok (toshok@ximian.com)
//

using System;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace PintoNS.General {

	public static class Keyboard {
		public static void AddGotKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (GotKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (GotKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemoveGotKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (GotKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (GotKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void AddLostKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (LostKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (LostKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemoveLostKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (LostKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (LostKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void AddPreviewGotKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (PreviewGotKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (PreviewGotKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemovePreviewGotKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (PreviewGotKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (PreviewGotKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void AddPreviewLostKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (PreviewLostKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (PreviewLostKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemovePreviewLostKeyboardFocusHandler (DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (PreviewLostKeyboardFocusEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (PreviewLostKeyboardFocusEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void AddKeyDownHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (KeyDownEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (KeyDownEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemoveKeyDownHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (KeyDownEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (KeyDownEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void AddKeyUpHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (KeyUpEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (KeyUpEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemoveKeyUpHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (KeyUpEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (KeyUpEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void AddPreviewKeyDownHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (PreviewKeyDownEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (PreviewKeyDownEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemovePreviewKeyDownHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (PreviewKeyDownEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (PreviewKeyDownEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void AddPreviewKeyUpHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).AddHandler (PreviewKeyUpEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).AddHandler (PreviewKeyUpEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static void RemovePreviewKeyUpHandler (DependencyObject element, KeyEventHandler handler)
		{
			if (element == null) throw new ArgumentNullException ("element");
			if (handler == null) throw new ArgumentNullException ("handler");

			if (element is UIElement)
				((UIElement)element).RemoveHandler (PreviewKeyUpEvent, handler);
			else if (element is ContentElement)
				((ContentElement)element).RemoveHandler (PreviewKeyUpEvent, handler);
			else
				throw new NotSupportedException ();
		}

		public static IInputElement Focus (IInputElement element)
		{
			return PrimaryDevice.Focus (element);
		}

		public static KeyStates GetKeyStates (Key key)
		{
			return PrimaryDevice.GetKeyStates (key);
		}

		public static bool IsKeyDown (Key key)
		{
			return PrimaryDevice.IsKeyDown (key);
		}

		public static bool IsKeyToggled (Key key)
		{
			return PrimaryDevice.IsKeyToggled (key);
		}

		public static bool IsKeyUp (Key key)
		{
			return PrimaryDevice.IsKeyUp (key);
		}

		public static IInputElement FocusedElement {
			get { return PrimaryDevice.FocusedElement; }
		}

		public static ModifierKeys Modifiers {
			get { return PrimaryDevice.Modifiers; }
		}

		public static KeyboardDevice PrimaryDevice {
			[SecurityCritical]
			get { return InputManager.Current.PrimaryKeyboardDevice; }
		}

		public static readonly RoutedEvent GotKeyboardFocusEvent =
			EventManager.RegisterRoutedEvent ("GotKeyboardFocus",
							  RoutingStrategy.Bubble,
							  typeof (KeyboardFocusChangedEventHandler),
							  typeof (Keyboard));

		public static readonly RoutedEvent LostKeyboardFocusEvent =
			EventManager.RegisterRoutedEvent ("LostKeyboardFocus",
							  RoutingStrategy.Bubble,
							  typeof (KeyboardFocusChangedEventHandler),
							  typeof (Keyboard));

		public static readonly RoutedEvent KeyDownEvent =
			EventManager.RegisterRoutedEvent ("KeyDown",
							  RoutingStrategy.Bubble,
							  typeof (KeyEventHandler),
							  typeof (Keyboard));

		public static readonly RoutedEvent KeyUpEvent =
			EventManager.RegisterRoutedEvent ("KeyUp",
							  RoutingStrategy.Bubble,
							  typeof (KeyEventHandler),
							  typeof (Keyboard));


		public static readonly RoutedEvent PreviewGotKeyboardFocusEvent =
			EventManager.RegisterRoutedEvent ("PreviewGotKeyboardFocus",
							  RoutingStrategy.Tunnel,
							  typeof (KeyboardFocusChangedEventHandler),
							  typeof (Keyboard));

		public static readonly RoutedEvent PreviewLostKeyboardFocusEvent =
			EventManager.RegisterRoutedEvent ("PreviewLostKeyboardFocus",
							  RoutingStrategy.Tunnel,
							  typeof (KeyboardFocusChangedEventHandler),
							  typeof (Keyboard));

		public static readonly RoutedEvent PreviewKeyDownEvent =
			EventManager.RegisterRoutedEvent ("PreviewKeyDown",
							  RoutingStrategy.Tunnel,
							  typeof (KeyEventHandler),
							  typeof (Keyboard));

		public static readonly RoutedEvent PreviewKeyUpEvent =
			EventManager.RegisterRoutedEvent ("PreviewKeyUp",
							  RoutingStrategy.Tunnel,
							  typeof (KeyEventHandler),
							  typeof (Keyboard));
	}

}
