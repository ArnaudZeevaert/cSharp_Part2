﻿#pragma checksum "..\..\fenetreDeConnexion.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C62D8096127557A94351D4E2599175A8A5C17EC14A11F2E502397FA2AEB2B7F8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using fenetreDeConnexion;


namespace fenetreDeConnexion {
    
    
    /// <summary>
    /// FenetreDeConnexion
    /// </summary>
    public partial class FenetreDeConnexion : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\fenetreDeConnexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NOM_TextBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\fenetreDeConnexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PRENOM_TextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\fenetreDeConnexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EMAIL_TextBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\fenetreDeConnexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LOGIN_Button;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\fenetreDeConnexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button QUITTER_Button;
        
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
            System.Uri resourceLocater = new System.Uri("/fenetreDeConnexion;component/fenetredeconnexion.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\fenetreDeConnexion.xaml"
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
            this.NOM_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.PRENOM_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.EMAIL_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.LOGIN_Button = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\fenetreDeConnexion.xaml"
            this.LOGIN_Button.Click += new System.Windows.RoutedEventHandler(this.Button_LOGIN_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.QUITTER_Button = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\fenetreDeConnexion.xaml"
            this.QUITTER_Button.Click += new System.Windows.RoutedEventHandler(this.QUITTER_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

