﻿#pragma checksum "..\..\tasks.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "864C44AA0EF748E32F915DF5DA8AAC5CE5CEC5AC5B4622CD22DC236752B0EF6E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Interface_1._0;
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


namespace Interface_1._0 {
    
    
    /// <summary>
    /// tasks
    /// </summary>
    public partial class tasks : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\tasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel RHDP;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\tasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\tasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Polyline RollUp;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\tasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_Close;
        
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
            System.Uri resourceLocater = new System.Uri("/Interface 1.0;component/tasks.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\tasks.xaml"
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
            this.RHDP = ((System.Windows.Controls.DockPanel)(target));
            
            #line 21 "..\..\tasks.xaml"
            this.RHDP.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Mouse_Drag_Window);
            
            #line default
            #line hidden
            return;
            case 2:
            this.title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            
            #line 43 "..\..\tasks.xaml"
            ((System.Windows.Controls.Label)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Mouse_Enter);
            
            #line default
            #line hidden
            
            #line 44 "..\..\tasks.xaml"
            ((System.Windows.Controls.Label)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Mouse_Leave);
            
            #line default
            #line hidden
            
            #line 45 "..\..\tasks.xaml"
            ((System.Windows.Controls.Label)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Roll_Up);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RollUp = ((System.Windows.Shapes.Polyline)(target));
            return;
            case 5:
            this.lbl_Close = ((System.Windows.Controls.Label)(target));
            
            #line 52 "..\..\tasks.xaml"
            this.lbl_Close.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Mouse_Enter_Close);
            
            #line default
            #line hidden
            
            #line 53 "..\..\tasks.xaml"
            this.lbl_Close.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Mouse_Leave_Close);
            
            #line default
            #line hidden
            
            #line 54 "..\..\tasks.xaml"
            this.lbl_Close.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Close_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 76 "..\..\tasks.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
