"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[311],{9492:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>l,contentTitle:()=>u,default:()=>m,frontMatter:()=>s,metadata:()=>i,toc:()=>c});const i=JSON.parse('{"id":"extensibility/customization","title":"Customization","description":"You can add you own customizations on top of the AwexpectCustomization class by adding extension methods.","source":"@site/docs/extensibility/customization.md","sourceDirName":"extensibility","slug":"/extensibility/customization","permalink":"/aweXpect/docs/extensibility/customization","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/extensibility/customization.md","tags":[],"version":"current","sidebarPosition":2,"frontMatter":{"sidebar_position":2},"sidebar":"tutorialSidebar","previous":{"title":"Write your own extension","permalink":"/aweXpect/docs/extensibility/write-extensions"}}');var o=n(4848),a=n(8453);const s={sidebar_position:2},u="Customization",l={},c=[{value:"Add a simple customization value",id:"add-a-simple-customization-value",level:2},{value:"Add a customization group",id:"add-a-customization-group",level:2}];function r(e){const t={code:"code",em:"em",h1:"h1",h2:"h2",header:"header",p:"p",pre:"pre",...(0,a.R)(),...e.components};return(0,o.jsxs)(o.Fragment,{children:[(0,o.jsx)(t.header,{children:(0,o.jsx)(t.h1,{id:"customization",children:"Customization"})}),"\n",(0,o.jsxs)(t.p,{children:["You can add you own customizations on top of the ",(0,o.jsx)(t.code,{children:"AwexpectCustomization"})," class by adding extension methods."]}),"\n",(0,o.jsx)(t.h2,{id:"add-a-simple-customization-value",children:"Add a simple customization value"}),"\n",(0,o.jsxs)(t.p,{children:["You can add a simple customizable value (e.g. an ",(0,o.jsx)(t.code,{children:"int"}),"):"]}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"public static class MyCustomizationExtensions\n{\n    public static ICustomizationValueSetter<int> MyCustomization(this AwexpectCustomization awexpectCustomization)\n        => new CustomizationValue<int>(awexpectCustomization, nameof(MyCustomization), 42);\n\n    internal class CustomizationValue<TValue>(IAwexpectCustomization awexpectCustomization, string key, TValue defaultValue)\n        : ICustomizationValueSetter<TValue>\n    {\n        public TValue Get()\n            => awexpectCustomization.Get(key, defaultValue);\n\n        public CustomizationLifetime Set(TValue value)\n            => awexpectCustomization.Set(key, value);\n    }\n}\n"})}),"\n",(0,o.jsx)(t.p,{children:"This allows expectations to access the value:"}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:" // will return the default value of 42\nint myCustomization = Customize.aweXpect.MyCustomization().Get();\n"})}),"\n",(0,o.jsx)(t.p,{children:"And users can customize the value:"}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"using (Customize.aweXpect.MyCustomization().Set(43))\n{\n    // will now return 43\n    int myCustomization = Customize.aweXpect.MyCustomization().Get();\n}\n// will now return again the default value of 42, because the customization lifetime was disposed\n_ = Customize.aweXpect.MyCustomization().Get();\n"})}),"\n",(0,o.jsx)(t.p,{children:(0,o.jsx)(t.em,{children:"Note: you can also use this mechanism for complex objects like classes, but they can only be changed as a whole (and not individual properties)"})}),"\n",(0,o.jsx)(t.h2,{id:"add-a-customization-group",children:"Add a customization group"}),"\n",(0,o.jsx)(t.p,{children:"You can also add a group of customization values, that can be changed individually or as a whole"}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"public static class JsonAwexpectCustomizationExtensions\n{\n    public static JsonCustomization Json(this AwexpectCustomization awexpectCustomization)\n        => new(awexpectCustomization);\n\n    public class JsonCustomization : ICustomizationValueUpdater<JsonCustomizationValue>\n    {\n        private readonly IAwexpectCustomization _awexpectCustomization;\n\n        internal JsonCustomization(IAwexpectCustomization awexpectCustomization)\n        {\n            _awexpectCustomization = awexpectCustomization;\n            DefaultJsonDocumentOptions = new CustomizationValue<JsonDocumentOptions>(\n                () => Get().DefaultJsonDocumentOptions,\n                v => Update(p => p with { DefaultJsonDocumentOptions = v }));\n            DefaultJsonSerializerOptions = new CustomizationValue<JsonSerializerOptions>(\n                () => Get().DefaultJsonSerializerOptions,\n                v => Update(p => p with { DefaultJsonSerializerOptions = v }));\n        }\n\n        public ICustomizationValueSetter<JsonDocumentOptions> DefaultJsonDocumentOptions { get; }\n        public ICustomizationValueSetter<JsonSerializerOptions> DefaultJsonSerializerOptions { get; }\n\n        public JsonCustomizationValue Get()\n            => _awexpectCustomization.Get(nameof(Json), new JsonCustomizationValue());\n\n        public CustomizationLifetime Update(Func<JsonCustomizationValue, JsonCustomizationValue> update)\n            => _awexpectCustomization.Set(nameof(Json), update(Get()));\n    }\n\n    public record JsonCustomizationValue\n    {\n        public JsonDocumentOptions DefaultJsonDocumentOptions { get; set; } = new()\n        {\n            AllowTrailingCommas = true\n        };\n        public JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } = new()\n        {\n            AllowTrailingCommas = true\n        };\n    }\n\n    private class CustomizationValue<TValue>(\n        Func<TValue> getter,\n        Func<TValue, CustomizationLifetime> setter)\n        : ICustomizationValueSetter<TValue>\n    {\n        public TValue Get() => getter();\n        public CustomizationLifetime Set(TValue value) => setter(value);\n    }\n}\n"})}),"\n",(0,o.jsx)(t.p,{children:"This allows expectations to access values either individually or for the whole group:"}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:" // both will return the default value 'true'\nint myCustomization1 = Customize.aweXpect.Json().Get().DefaultJsonDocumentOptions.AllowTrailingCommas;\nint myCustomization2 = Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get().AllowTrailingCommas;\n"})}),"\n",(0,o.jsx)(t.p,{children:"And users can customize either individual values or the whole group:"}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"// update a single value (keeping the other values)\nJsonSerializerOptions mySerializerOptions = new();\nusing (Customize.aweXpect.Json().DefaultJsonSerializerOptions.Set(mySerializerOptions))\n{\n    // will use `mySerializerOptions` for the `JsonSerializerOptions`\n\t// but keep any configured `JsonDocumentOptions`\n}\n\n// ...or update the whole group\nJsonCustomizationValue myCustomization = new();\nusing (Customize.aweXpect.Json().Update(_ => myCustomization))\n{\n    // will use the all set properties from the `myCustomization`\n}\n"})})]})}function m(e={}){const{wrapper:t}={...(0,a.R)(),...e.components};return t?(0,o.jsx)(t,{...e,children:(0,o.jsx)(r,{...e})}):r(e)}},8453:(e,t,n)=>{n.d(t,{R:()=>s,x:()=>u});var i=n(6540);const o={},a=i.createContext(o);function s(e){const t=i.useContext(a);return i.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function u(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(o):e.components||o:s(e.components),i.createElement(a.Provider,{value:t},e.children)}}}]);