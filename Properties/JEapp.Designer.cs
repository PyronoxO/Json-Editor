﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Json_Editor.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.9.0.0")]
    internal sealed partial class JEapp : global::System.Configuration.ApplicationSettingsBase {
        
        private static JEapp defaultInstance = ((JEapp)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new JEapp())));
        
        public static JEapp Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("32, 32, 32")]
        public global::System.Drawing.Color BackColor {
            get {
                return ((global::System.Drawing.Color)(this["BackColor"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0, 0")]
        public global::System.Drawing.Point StartLocation {
            get {
                return ((global::System.Drawing.Point)(this["StartLocation"]));
            }
            set {
                this["StartLocation"] = value;
            }
        }
    }
}
