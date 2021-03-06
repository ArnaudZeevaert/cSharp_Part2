﻿#pragma checksum "..\..\AboutBox.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AD260AA7748257AF690AC4F11D624777AA916947F123214058E6491F7ADE6B7E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PersonalMap_Manager;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PersonalMap_Manager {
    
    
    /// <summary>
    /// AboutBox
    /// </summary>
    public partial class AboutBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SystemInfoWindowOperatingSystem;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SystemInfoWindowNetFrameworkVersion;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SystemInfoWindowWindowsUserName;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SystemInfoWindowDomainName;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SystemInfoWindowProcessor;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SystemInfoWindowLanIp;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SystemInfoWindowRubyVersion;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\AboutBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockInfoPersonneConnectee;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PersonalMap_Manager;component/aboutbox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AboutBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 24 "..\..\AboutBox.xaml"
            ((System.Windows.Controls.Primitives.Thumb)(target)).DragDelta += new System.Windows.Controls.Primitives.DragDeltaEventHandler(this.MainHeaderThumb_OnDragDelta);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 27 "..\..\AboutBox.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClose_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SystemInfoWindowOperatingSystem = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.SystemInfoWindowNetFrameworkVersion = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.SystemInfoWindowWindowsUserName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.SystemInfoWindowDomainName = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.SystemInfoWindowProcessor = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.SystemInfoWindowLanIp = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.SystemInfoWindowRubyVersion = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.TextBlockInfoPersonneConnectee = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

