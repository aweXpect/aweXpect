"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[9595],{2960:(e,n,t)=>{t.r(n),t.d(n,{assets:()=>r,contentTitle:()=>i,default:()=>d,frontMatter:()=>o,metadata:()=>s,toc:()=>l});const s=JSON.parse('{"id":"expectations/json","title":"JSON","description":"Describes the possible expectations for working","source":"@site/docs/expectations/json.md","sourceDirName":"expectations","slug":"/expectations/json","permalink":"/aweXpect/docs/expectations/json","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/json.md","tags":[],"version":"current","sidebarPosition":17,"frontMatter":{"sidebar_position":17},"sidebar":"tutorialSidebar","previous":{"title":"Callbacks","permalink":"/aweXpect/docs/expectations/callbacks"},"next":{"title":"Extensibility","permalink":"/aweXpect/docs/category/extensibility"}}');var a=t(4848),c=t(8453);const o={sidebar_position:17},i="JSON",r={},l=[{value:"String comparison as JSON",id:"string-comparison-as-json",level:2},{value:"Validation",id:"validation",level:2},{value:"<code>JsonElement</code>",id:"jsonelement",level:2},{value:"Match",id:"match",level:3},{value:"Be object",id:"be-object",level:3},{value:"Be array",id:"be-array",level:3},{value:"JSON serializable",id:"json-serializable",level:2}];function h(e){const n={a:"a",code:"code",h1:"h1",h2:"h2",h3:"h3",header:"header",p:"p",pre:"pre",...(0,c.R)(),...e.components};return(0,a.jsxs)(a.Fragment,{children:[(0,a.jsx)(n.header,{children:(0,a.jsx)(n.h1,{id:"json",children:"JSON"})}),"\n",(0,a.jsxs)(n.p,{children:["Describes the possible expectations for working\nwith ",(0,a.jsx)(n.a,{href:"https://learn.microsoft.com/en-us/dotnet/api/system.text.json",children:"System.Text.Json"}),"."]}),"\n",(0,a.jsx)(n.h2,{id:"string-comparison-as-json",children:"String comparison as JSON"}),"\n",(0,a.jsx)(n.p,{children:"You can compare two strings for JSON equivalency:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "{\\"foo\\":{\\"bar\\":[1,2,3]}}";\nstring expected = """\n                  {\n                    "foo": {\n                      "bar": [ 1, 2, 3 ]\n                    }\n                  }\n                  """;\n\nawait Expect.That(subject).Is(expected).AsJson();\n'})}),"\n",(0,a.jsx)(n.h2,{id:"validation",children:"Validation"}),"\n",(0,a.jsx)(n.p,{children:"You can verify, that a string is valid JSON."}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "{\\"foo\\": 2}";\n\nawait Expect.That(subject).IsValidJson();\n'})}),"\n",(0,a.jsxs)(n.p,{children:["This verifies that the string can be parsed by ",(0,a.jsxs)(n.a,{href:"https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsondocument.parse",children:["\n",(0,a.jsx)(n.code,{children:"JsonDocument.Parse"})]})," without\nexceptions."]}),"\n",(0,a.jsxs)(n.p,{children:["You can also specify the ",(0,a.jsxs)(n.a,{href:"https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsondocumentoptions",children:["\n",(0,a.jsx)(n.code,{children:"JsonDocumentOptions"})]}),":"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "{\\"foo\\": 2}";\n\nawait Expect.That(subject).IsValidJson(o => o with {CommentHandling = JsonCommentHandling.Disallow});\n'})}),"\n",(0,a.jsxs)(n.p,{children:["You can also add additional expectations on the ",(0,a.jsxs)(n.a,{href:"https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonelement",children:["\n",(0,a.jsx)(n.code,{children:"JsonElement"})]})," created when parsing the\nsubject:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "{\\"foo\\": 2}";\n\nawait Expect.That(subject).IsValidJson().Which(j => j.Matches(new{foo = 2}));\n'})}),"\n",(0,a.jsx)(n.h2,{id:"jsonelement",children:(0,a.jsx)(n.code,{children:"JsonElement"})}),"\n",(0,a.jsx)(n.h3,{id:"match",children:"Match"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"JsonElement"})," matches an expected object:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("{\\"foo\\": 1, \\"bar\\": \\"baz\\"}").RootElement;\n\nawait Expect.That(subject).Matches(new{foo = 1});\nawait Expect.That(subject).MatchesExactly(new{foo = 1, bar = "baz"});\n'})}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"JsonElement"})," matches an expected array:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("[1,2,3]").RootElement;\n\nawait Expect.That(subject).Matches([1, 2]);\nawait Expect.That(subject).MatchesExactly([1, 2, 3]);\n'})}),"\n",(0,a.jsxs)(n.p,{children:["You can also verify, that the ",(0,a.jsx)(n.code,{children:"JsonElement"})," matches a primitive type:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(JsonDocument.Parse("\\"foo\\"").RootElement).Matches("foo");\nawait Expect.That(JsonDocument.Parse("42.3").RootElement).Matches(42.3);\nawait Expect.That(JsonDocument.Parse("true").RootElement).Matches(true);\nawait Expect.That(JsonDocument.Parse("null").RootElement).Matches(null);\n'})}),"\n",(0,a.jsx)(n.h3,{id:"be-object",children:"Be object"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify that a ",(0,a.jsx)(n.code,{children:"JsonElement"})," is a JSON object that satisfy some expectations:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("{\\"foo\\": 1, \\"bar\\": \\"baz\\"}").RootElement;\n\nawait Expect.That(subject).IsObject(o => o\n    .With("foo").Matching(1).And\n    .With("bar").Matching("baz"));\n'})}),"\n",(0,a.jsx)(n.p,{children:"You can also verify that a property is another object recursively:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("{\\"foo\\": {\\"bar\\": \\"baz\\"}}").RootElement;\n\nawait Expect.That(subject).IsObject(o => o\n    .With("foo").AnObject(i => i\n        .With("bar").Matching("baz")));\n'})}),"\n",(0,a.jsx)(n.p,{children:"You can also verify that a property is an array:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("{\\"foo\\": [1, 2]}").RootElement;\n\nawait Expect.That(subject).IsObject(o => o\n    .With("foo").AnArray(a => a.WithElements(1, 2)));\n'})}),"\n",(0,a.jsx)(n.p,{children:"You can also verify the number of properties in a JSON object:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("{\\"foo\\": 1, \\"bar\\": \\"baz\\"}").RootElement;\n\nawait Expect.That(subject).IsObject(o => o.With(2).Properties());\n'})}),"\n",(0,a.jsx)(n.h3,{id:"be-array",children:"Be array"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify that a ",(0,a.jsx)(n.code,{children:"JsonElement"})," is a JSON array that satisfy some expectations:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("[\\"foo\\",\\"bar\\"]").RootElement;\n\nawait Expect.That(subject).IsArray(a => a\n    .At(0).Matching("foo").And\n    .At(1).Matching("bar"));\n'})}),"\n",(0,a.jsx)(n.p,{children:"You can also verify the number of elements in a JSON array:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("[1, 2, 3]").RootElement;\n\nawait Expect.That(subject).IsArray(o => o.With(3).Elements());\n'})}),"\n",(0,a.jsx)(n.p,{children:"You can also directly match the expected values of an array:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("[\\"foo\\",\\"bar\\"]").RootElement;\n\nawait Expect.That(subject).IsArray(a => a\n    .WithElements("foo", "bar"));\n'})}),"\n",(0,a.jsxs)(n.p,{children:["You can also match sub-arrays recursively (add ",(0,a.jsx)(n.code,{children:"null"})," to skip an element):"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse("[[0,1,2],[1,2,3],[2,3,4],[3,4,5,6]]").RootElement;\n\nawait Expect.That(subject).IsArray(a => a\n    .WithArrays(\n        i => i.WithElements(0,1,2),\n        i => i.At(0).Matching(1).And.At(2).Matching(3),\n        null,\n        i => i.With(4).Elements()\n    ));\n'})}),"\n",(0,a.jsxs)(n.p,{children:["You can also match objects recursively (add ",(0,a.jsx)(n.code,{children:"null"})," to skip an element):"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'JsonElement subject = JsonDocument.Parse(\n\t"""\n\t[\n\t  {"foo":1},\n\t  {"bar":2},\n\t  {"bar": null, "baz": true}\n\t]\n\t""").RootElement;\nawait Expect.That(subject).IsArray(a => a\n\t.WithObjects(\n\t\ti => i.With("foo").Matching(1),\n\t\tnull,\n\t\ti => i.With(2).Properties()\n\t));\n'})}),"\n",(0,a.jsx)(n.h2,{id:"json-serializable",children:"JSON serializable"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify that an ",(0,a.jsx)(n.code,{children:"object"})," is JSON serializable:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:"MyClass subject = new MyClass();\n\nawait Expect.That(subject).IsJsonSerializable();\n"})}),"\n",(0,a.jsxs)(n.p,{children:["This validates, that the ",(0,a.jsx)(n.code,{children:"MyClass"})," can be serialized and deserialized to/from JSON and that the result is equivalent to\nthe original subject."]}),"\n",(0,a.jsxs)(n.p,{children:["You can specify both, the ",(0,a.jsxs)(n.a,{href:"https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions",children:["\n",(0,a.jsx)(n.code,{children:"JsonSerializerOptions"})]})," and the\nequivalency options:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'MyClass subject = new MyClass();\n\nawait Expect.That(subject).IsJsonSerializable(\n    new JsonSerializerOptions { IncludeFields = true },\n    e => e.IgnoringMember("Foo"));\n'})}),"\n",(0,a.jsx)(n.p,{children:"You can also specify an expected generic type that the subject should have:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'object subject = //...\n\nawait Expect.That(subject).IsJsonSerializable<MyClass>(\n    new JsonSerializerOptions { IncludeFields = true },\n    e => e.IgnoringMember("Foo"));\n'})})]})}function d(e={}){const{wrapper:n}={...(0,c.R)(),...e.components};return n?(0,a.jsx)(n,{...e,children:(0,a.jsx)(h,{...e})}):h(e)}},8453:(e,n,t)=>{t.d(n,{R:()=>o,x:()=>i});var s=t(6540);const a={},c=s.createContext(a);function o(e){const n=s.useContext(c);return s.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function i(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(a):e.components||a:o(e.components),s.createElement(c.Provider,{value:n},e.children)}}}]);