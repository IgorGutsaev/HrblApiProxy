﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Filuet.Hrbl.Ordering.POC.PromoEngine.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Filuet.Hrbl.Ordering.POC.PromoEngine.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {&quot;IsPromoOrder&quot;:&quot;Y&quot;,&quot;Promotions&quot;:{&quot;Promotion&quot;:[{&quot;DistributorId&quot;:&quot;V7003827&quot;,&quot;OrderMonth&quot;:&quot;2022/01&quot;,&quot;OrderCount&quot;:0,&quot;DistributorStatus&quot;:&quot;COMPLETE&quot;,&quot;DOB&quot;:null,&quot;Country&quot;:&quot;CY&quot;,&quot;DistributorType&quot;:&quot;SP&quot;,&quot;DistributorSubType&quot;:&quot;MT&quot;,&quot;SKU&quot;:&quot;0144&quot;,&quot;ShippingInstructions&quot;:null,&quot;SKUReward&quot;:&quot;Y&quot;,&quot;ApplicationDate&quot;:null,&quot;AnniversaryMonth&quot;:null,&quot;FreightCode&quot;:null,&quot;PromotionRule&quot;:&quot;Mandatory Mixed Promotion SKU\u002BCV, ONE item to be reedem[Promotion name]&quot;,&quot;Precedence&quot;:null,&quot;OrderType&quot;:null,&quot;OrderedQuantity&quot;:3,&quot;ChrAttribute1&quot;:&quot;ONE [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string cachedPromotions {
            get {
                return ResourceManager.GetString("cachedPromotions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [
        ///   {
        ///      &quot;ruleId&quot;:&quot;0001&quot;,
        ///      &quot;ruleName&quot;:&quot;AUTOMATIC Cash Voucher Promotion [Promotion name]&quot;,
        ///      &quot;redemptionType&quot;:&quot;AUTOMATIC&quot;,
        ///      &quot;redemptionLimit&quot;:&quot;ONE&quot;,
        ///      &quot;rewards&quot;:[
        ///         {
        ///            &quot;validity&quot;:&quot;&quot;,
        ///            &quot;type&quot;:&quot;CASH VOUCHER&quot;,
        ///            &quot;ruleName&quot;:&quot;AUTOMATIC Cash Voucher Promotion [Promotion name]&quot;,
        ///            &quot;reward&quot;:&quot;11&quot;,
        ///            &quot;description&quot;:&quot;Cash Voucher [Reward Description]&quot;,
        ///            &quot;qty&quot;:1,
        ///            &quot;maxQty&quot;:0,
        ///			&quot;cashAmount&quot;:11.0
        ///       [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string mockedPromotions {
            get {
                return ResourceManager.GetString("mockedPromotions", resourceCulture);
            }
        }
    }
}
