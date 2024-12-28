"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[5187],{371:(e,n,a)=>{a.r(n),a.d(n,{assets:()=>l,contentTitle:()=>s,default:()=>h,frontMatter:()=>i,metadata:()=>c,toc:()=>o});const c=JSON.parse('{"id":"expectations/callbacks","title":"Callbacks","description":"Describes the possible expectations for working with callbacks.","source":"@site/docs/expectations/callbacks.md","sourceDirName":"expectations","slug":"/expectations/callbacks","permalink":"/aweXpect/docs/expectations/callbacks","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/callbacks.md","tags":[],"version":"current","sidebarPosition":16,"frontMatter":{"sidebar_position":16},"sidebar":"tutorialSidebar","previous":{"title":"Events","permalink":"/aweXpect/docs/expectations/events"},"next":{"title":"Extensibility","permalink":"/aweXpect/docs/category/extensibility"}}');var t=a(4848),r=a(8453);const i={sidebar_position:16},s="Callbacks",l={},o=[{value:"Recording",id:"recording",level:2},{value:"Timeout",id:"timeout",level:3},{value:"Amount",id:"amount",level:3},{value:"Parameters",id:"parameters",level:3}];function d(e){const n={code:"code",em:"em",h1:"h1",h2:"h2",h3:"h3",header:"header",p:"p",pre:"pre",...(0,r.R)(),...e.components};return(0,t.jsxs)(t.Fragment,{children:[(0,t.jsx)(n.header,{children:(0,t.jsx)(n.h1,{id:"callbacks",children:"Callbacks"})}),"\n",(0,t.jsx)(n.p,{children:"Describes the possible expectations for working with callbacks."}),"\n",(0,t.jsx)(n.h2,{id:"recording",children:"Recording"}),"\n",(0,t.jsx)(n.p,{children:'First, you have to start a recording of callbacks. This method is available in the "aweXpect.Recording" namespace.'}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"// \u2193 Record callbacks without parameters\nICallbackRecording recording = Record.Callback();\nICallbackRecording<string> recording = Record.Callback<string>();\n// \u2191 Records callbacks with a string parameter\n"})}),"\n",(0,t.jsx)(n.p,{children:"Then you can trigger the callback on the recording."}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"class MyClass\n{\n  public void Execute(Action<string> onCompleted)\n  {\n    // do something in a background thread and then call the onCompleted callback\n  }\n}\n\nsut.Execute(v => recording.Trigger(v));\n"})}),"\n",(0,t.jsx)(n.p,{children:"At last you can wait for the callback to be triggered:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(recording).Should().Trigger();\n"})}),"\n",(0,t.jsx)(n.p,{children:"You can also verify that the callback will not be triggered:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(recording).Should().NotTrigger();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsx)(n.em,{children:"NOTE: The last statement will result never return, unless a timeout or cancellation is specified. Therefore, when\nnothing is specified, a default timeout of 30 seconds is applied!"})}),"\n",(0,t.jsx)(n.h3,{id:"timeout",children:"Timeout"}),"\n",(0,t.jsx)(n.p,{children:"You can specify a timeout, how long you want to wait for the callback to be triggered:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(recording).Should().Trigger().Within(TimeSpan.FromSeconds(5))\n  .Because("it should take at most 5 seconds to complete");\n'})}),"\n",(0,t.jsxs)(n.p,{children:["Alternatively you can also use a ",(0,t.jsx)(n.code,{children:"CancellationToken"})," for a timeout:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'CancellationToken cancellationToken = new CancellationTokenSource(5000).Token;\nawait Expect.That(recording).Should().Trigger().WithCancellation(cancellationToken)\n  .Because("it should be completed, before the cancellationToken is cancelled");\n'})}),"\n",(0,t.jsx)(n.h3,{id:"amount",children:"Amount"}),"\n",(0,t.jsx)(n.p,{children:"You can specify a number of times, that a callback must at least be executed:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(recording).Should().Trigger(3.Times());\n"})}),"\n",(0,t.jsx)(n.p,{children:"You can also verify, that the callback was not executed at least the given number of times:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(recording).Should().NotTrigger(3.Times());\n"})}),"\n",(0,t.jsx)(n.h3,{id:"parameters",children:"Parameters"}),"\n",(0,t.jsx)(n.p,{children:"You can also use callbacks with a single parameter:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'ICallbackRecording<string> recording = Record.Callback<string>();\n\nrecording.Trigger("foo");\nrecording.Trigger("bar");\n\nawait That(recording).Should().Trigger(2.Times());\n'})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsx)(n.em,{children:"In case of a failed expectation, the recorded parameters will be displayed in the error message."})})]})}function h(e={}){const{wrapper:n}={...(0,r.R)(),...e.components};return n?(0,t.jsx)(n,{...e,children:(0,t.jsx)(d,{...e})}):d(e)}},8453:(e,n,a)=>{a.d(n,{R:()=>i,x:()=>s});var c=a(6540);const t={},r=c.createContext(t);function i(e){const n=c.useContext(r);return c.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function s(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(t):e.components||t:i(e.components),c.createElement(r.Provider,{value:n},e.children)}}}]);