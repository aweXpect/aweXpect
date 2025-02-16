"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[4801],{6391:(e,n,t)=>{t.r(n),t.d(n,{assets:()=>u,contentTitle:()=>l,default:()=>d,frontMatter:()=>o,metadata:()=>i,toc:()=>r});const i=JSON.parse('{"id":"extensions/write-extensions","title":"Write your own extension","description":"This library will never be able to cope with all ideas and use cases. Therefore, it is possible to use the [","source":"@site/docs/extensions/01-write-extensions.md","sourceDirName":"extensions","slug":"/extensions/write-extensions","permalink":"/docs/extensions/write-extensions","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/extensions/01-write-extensions.md","tags":[],"version":"current","sidebarPosition":1,"frontMatter":{},"sidebar":"extensionsSidebar","previous":{"title":"HTTP","permalink":"/docs/extensions/project/Web/http"}}');var s=t(4848),a=t(8453);const o={},l="Write your own extension",u={},r=[{value:"Example",id:"example",level:2},{value:"Constraints",id:"constraints",level:2},{value:"Customization",id:"customization",level:2},{value:"Add a simple customization value",id:"add-a-simple-customization-value",level:3},{value:"Add a customization group",id:"add-a-customization-group",level:3}];function c(e){const n={a:"a",br:"br",code:"code",em:"em",h1:"h1",h2:"h2",h3:"h3",header:"header",li:"li",p:"p",pre:"pre",ul:"ul",...(0,a.R)(),...e.components};return(0,s.jsxs)(s.Fragment,{children:[(0,s.jsx)(n.header,{children:(0,s.jsx)(n.h1,{id:"write-your-own-extension",children:"Write your own extension"})}),"\n",(0,s.jsxs)(n.p,{children:["This library will never be able to cope with all ideas and use cases. Therefore, it is possible to use the ",(0,s.jsxs)(n.a,{href:"https://www.nuget.org/packages/aweXpect.Core/",children:["\n",(0,s.jsx)(n.code,{children:"aweXpect.Core"})]})," package and write your own extensions.\nGoal of this package is to be more stable than the main aweXpect package, so reduce the risk of version conflicts\nbetween different extensions."]}),"\n",(0,s.jsxs)(n.p,{children:["You can extend the functionality for any types, by adding extension methods on ",(0,s.jsx)(n.code,{children:"IThat<TType>"}),"."]}),"\n",(0,s.jsx)(n.h2,{id:"example",children:"Example"}),"\n",(0,s.jsx)(n.p,{children:"You want to verify that a string corresponds to an absolute path."}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'/// <summary>\n///     Verifies that the <paramref name="subject"/> is an absolute path.\n/// </summary>\npublic static AndOrResult<string, IThat<string>> IsAbsolutePath(\n  this IThat<string> subject)\n  => new(subject.ExpectationBuilder.AddConstraint(it\n      => new IsAbsolutePathConstraint(it)),\n    subject);\n\nprivate readonly struct IsAbsolutePathConstraint(string it) : IValueConstraint<string>\n{\n  public ConstraintResult IsMetBy(string actual)\n  {\n    var absolutePath = Path.GetFullPath(actual);\n    if (absolutePath == actual)\n    {\n      return new ConstraintResult.Success<string>(actual, "be an absolute path");\n    }\n\n    return new ConstraintResult.Failure("be an absolute path",\n      $"{it} found {Formatter.Format(actual)}");\n  }\n}\n'})}),"\n",(0,s.jsx)(n.h2,{id:"constraints",children:"Constraints"}),"\n",(0,s.jsxs)(n.p,{children:["The basis for expectations are constraints. You can add different constraints to the ",(0,s.jsx)(n.code,{children:"ExpectationBuilder"})," that is\navailable for the ",(0,s.jsx)(n.code,{children:"IThat<T>"}),". They differ in the input and output parameters:"]}),"\n",(0,s.jsxs)(n.ul,{children:["\n",(0,s.jsxs)(n.li,{children:[(0,s.jsx)(n.code,{children:"IValueConstraint<T>"}),(0,s.jsx)(n.br,{}),"\n","It receives the actual value ",(0,s.jsx)(n.code,{children:"T"})," and returns a ",(0,s.jsx)(n.code,{children:"ConstraintResult"}),"."]}),"\n",(0,s.jsxs)(n.li,{children:[(0,s.jsx)(n.code,{children:"IAsyncConstraint<T>"}),(0,s.jsx)(n.br,{}),"\n","It receives the actual value ",(0,s.jsx)(n.code,{children:"T"})," and a ",(0,s.jsx)(n.code,{children:"CancellationToken"})," and returns the ",(0,s.jsx)(n.code,{children:"ConstraintResult"})," asynchronously.",(0,s.jsx)(n.br,{}),"\n",(0,s.jsxs)(n.em,{children:["Use it when you need asynchronous functionality or access to the timeout ",(0,s.jsx)(n.code,{children:"CancellationToken"}),"."]})]}),"\n",(0,s.jsxs)(n.li,{children:[(0,s.jsx)(n.code,{children:"IContextConstraint<T>"})," / ",(0,s.jsx)(n.code,{children:"IAsyncContextConstraint<T>"}),(0,s.jsx)(n.br,{}),"\n","Similar to the ",(0,s.jsx)(n.code,{children:"IValueConstraint<T>"})," and ",(0,s.jsx)(n.code,{children:"IAsyncConstraint<T>"})," respectively but receives an additional\n",(0,s.jsx)(n.code,{children:"IEvaluationContext"})," parameter that allows storing and receiving data between expectations.",(0,s.jsx)(n.br,{}),"\n",(0,s.jsxs)(n.em,{children:["This mechanism is used for example to avoid enumerating an ",(0,s.jsx)(n.code,{children:"IEnumerable"})," multiple times across multiple constraints."]})]}),"\n"]}),"\n",(0,s.jsx)(n.h2,{id:"customization",children:"Customization"}),"\n",(0,s.jsxs)(n.p,{children:["You can add you own ",(0,s.jsx)(n.a,{href:"/docs/expectations/advanced/customization",children:"customizations"})," on top of the ",(0,s.jsx)(n.code,{children:"AwexpectCustomization"})," class by adding extension methods."]}),"\n",(0,s.jsx)(n.h3,{id:"add-a-simple-customization-value",children:"Add a simple customization value"}),"\n",(0,s.jsxs)(n.p,{children:["You can add a simple customizable value (e.g. an ",(0,s.jsx)(n.code,{children:"int"}),"):"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"public static class MyCustomizationExtensions\n{\n    public static ICustomizationValueSetter<int> MyCustomization(this AwexpectCustomization awexpectCustomization)\n        => new CustomizationValue<int>(awexpectCustomization, nameof(MyCustomization), 42);\n\n    internal class CustomizationValue<TValue>(IAwexpectCustomization awexpectCustomization, string key, TValue defaultValue)\n        : ICustomizationValueSetter<TValue>\n    {\n        public TValue Get()\n            => awexpectCustomization.Get(key, defaultValue);\n\n        public CustomizationLifetime Set(TValue value)\n            => awexpectCustomization.Set(key, value);\n    }\n}\n"})}),"\n",(0,s.jsx)(n.p,{children:"This allows expectations to access the value:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:" // will return the default value of 42\nint myCustomization = Customize.aweXpect.MyCustomization().Get();\n"})}),"\n",(0,s.jsx)(n.p,{children:"And users can customize the value:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"using (Customize.aweXpect.MyCustomization().Set(43))\n{\n    // will now return 43\n    int myCustomization = Customize.aweXpect.MyCustomization().Get();\n}\n// will now return again the default value of 42, because the customization lifetime was disposed\n_ = Customize.aweXpect.MyCustomization().Get();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsx)(n.em,{children:"Note: you can also use this mechanism for complex objects like classes, but they can only be changed as a whole (and\nnot individual properties)"})}),"\n",(0,s.jsx)(n.h3,{id:"add-a-customization-group",children:"Add a customization group"}),"\n",(0,s.jsx)(n.p,{children:"You can also add a group of customization values, that can be changed individually or as a whole"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"public static class JsonAwexpectCustomizationExtensions\n{\n    public static JsonCustomization Json(this AwexpectCustomization awexpectCustomization)\n        => new(awexpectCustomization);\n\n    public class JsonCustomization : ICustomizationValueUpdater<JsonCustomizationValue>\n    {\n        private readonly IAwexpectCustomization _awexpectCustomization;\n\n        internal JsonCustomization(IAwexpectCustomization awexpectCustomization)\n        {\n            _awexpectCustomization = awexpectCustomization;\n            DefaultJsonDocumentOptions = new CustomizationValue<JsonDocumentOptions>(\n                () => Get().DefaultJsonDocumentOptions,\n                v => Update(p => p with { DefaultJsonDocumentOptions = v }));\n            DefaultJsonSerializerOptions = new CustomizationValue<JsonSerializerOptions>(\n                () => Get().DefaultJsonSerializerOptions,\n                v => Update(p => p with { DefaultJsonSerializerOptions = v }));\n        }\n\n        public ICustomizationValueSetter<JsonDocumentOptions> DefaultJsonDocumentOptions { get; }\n        public ICustomizationValueSetter<JsonSerializerOptions> DefaultJsonSerializerOptions { get; }\n\n        public JsonCustomizationValue Get()\n            => _awexpectCustomization.Get(nameof(Json), new JsonCustomizationValue());\n\n        public CustomizationLifetime Update(Func<JsonCustomizationValue, JsonCustomizationValue> update)\n            => _awexpectCustomization.Set(nameof(Json), update(Get()));\n    }\n\n    public record JsonCustomizationValue\n    {\n        public JsonDocumentOptions DefaultJsonDocumentOptions { get; set; } = new()\n        {\n            AllowTrailingCommas = true\n        };\n        public JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } = new()\n        {\n            AllowTrailingCommas = true\n        };\n    }\n\n    private class CustomizationValue<TValue>(\n        Func<TValue> getter,\n        Func<TValue, CustomizationLifetime> setter)\n        : ICustomizationValueSetter<TValue>\n    {\n        public TValue Get() => getter();\n        public CustomizationLifetime Set(TValue value) => setter(value);\n    }\n}\n"})}),"\n",(0,s.jsx)(n.p,{children:"This allows expectations to access values either individually or for the whole group:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:" // both will return the default value 'true'\nint myCustomization1 = Customize.aweXpect.Json().Get().DefaultJsonDocumentOptions.AllowTrailingCommas;\nint myCustomization2 = Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get().AllowTrailingCommas;\n"})}),"\n",(0,s.jsx)(n.p,{children:"And users can customize either individual values or the whole group:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"// update a single value (keeping the other values)\nJsonSerializerOptions mySerializerOptions = new();\nusing (Customize.aweXpect.Json().DefaultJsonSerializerOptions.Set(mySerializerOptions))\n{\n    // will use `mySerializerOptions` for the `JsonSerializerOptions`\n\t// but keep any configured `JsonDocumentOptions`\n}\n\n// ...or update the whole group\nJsonCustomizationValue myCustomization = new();\nusing (Customize.aweXpect.Json().Update(_ => myCustomization))\n{\n    // will use the all set properties from the `myCustomization`\n}\n"})})]})}function d(e={}){const{wrapper:n}={...(0,a.R)(),...e.components};return n?(0,s.jsx)(n,{...e,children:(0,s.jsx)(c,{...e})}):c(e)}},8453:(e,n,t)=>{t.d(n,{R:()=>o,x:()=>l});var i=t(6540);const s={},a=i.createContext(s);function o(e){const n=i.useContext(a);return i.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function l(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(s):e.components||s:o(e.components),i.createElement(a.Provider,{value:n},e.children)}}}]);