﻿#pragma checksum "..\..\Friendlist.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B257EF9BD3794E9302F194008B6EBD64389B5ED1"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Register;
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
using System.Windows.Interactivity;
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


namespace Register {
    
    
    /// <summary>
    /// Friendlist
    /// </summary>
    public partial class Friendlist : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\Friendlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.ColorZone NavBar;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\Friendlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border myh;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\Friendlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button myhead;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\Friendlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Myname;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\Friendlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox gexingqianming;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\Friendlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Searchbox;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\Friendlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView FriendList;
        
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
            System.Uri resourceLocater = new System.Uri("/Register;component/friendlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Friendlist.xaml"
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
            this.NavBar = ((MaterialDesignThemes.Wpf.ColorZone)(target));
            
            #line 28 "..\..\Friendlist.xaml"
            this.NavBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.NavBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.myh = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.myhead = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\Friendlist.xaml"
            this.myhead.Click += new System.Windows.RoutedEventHandler(this.Head_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Myname = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.gexingqianming = ((System.Windows.Controls.TextBox)(target));
            
            #line 119 "..\..\Friendlist.xaml"
            this.gexingqianming.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Searchbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 125 "..\..\Friendlist.xaml"
            this.Searchbox.GotFocus += new System.Windows.RoutedEventHandler(this.Searchbox_GotFocus);
            
            #line default
            #line hidden
            
            #line 125 "..\..\Friendlist.xaml"
            this.Searchbox.LostFocus += new System.Windows.RoutedEventHandler(this.Searchbox_LostFocus);
            
            #line default
            #line hidden
            
            #line 126 "..\..\Friendlist.xaml"
            this.Searchbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Searchbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 149 "..\..\Friendlist.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 152 "..\..\Friendlist.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 9:
            this.FriendList = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

