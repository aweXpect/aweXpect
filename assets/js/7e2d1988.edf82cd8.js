"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[5187],{371:(e,n,a)=>{a.r(n),a.d(n,{assets:()=>o,contentTitle:()=>c,default:()=>h,frontMatter:()=>l,metadata:()=>s,toc:()=>r});const s=JSON.parse('{"id":"expectations/callbacks","title":"Callbacks","description":"Describes the possible expectations for working with callbacks.","source":"@site/docs/expectations/callbacks.md","sourceDirName":"expectations","slug":"/expectations/callbacks","permalink":"/aweXpect/docs/expectations/callbacks","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/callbacks.md","tags":[],"version":"current","sidebarPosition":16,"frontMatter":{"sidebar_position":16},"sidebar":"tutorialSidebar","previous":{"title":"Events","permalink":"/aweXpect/docs/expectations/events"},"next":{"title":"Extensibility","permalink":"/aweXpect/docs/category/extensibility"}}');var i=a(4848),t=a(8453);const l={sidebar_position:16},c="Callbacks",o={},r=[{value:"Recording",id:"recording",level:2},{value:"Timeout",id:"timeout",level:3},{value:"Amount",id:"amount",level:3},{value:"Parameters",id:"parameters",level:3}];function d(e){const n={code:"code",em:"em",h1:"h1",h2:"h2",h3:"h3",header:"header",p:"p",pre:"pre",...(0,t.R)(),...e.components};return(0,i.jsxs)(i.Fragment,{children:[(0,i.jsx)(n.header,{children:(0,i.jsx)(n.h1,{id:"callbacks",children:"Callbacks"})}),"\n",(0,i.jsx)(n.p,{children:"Describes the possible expectations for working with callbacks."}),"\n",(0,i.jsx)(n.h2,{id:"recording",children:"Recording"}),"\n",(0,i.jsx)(n.p,{children:'First, you have to start recording callback signals. This class is available in the "aweXpect.Recording" namespace.'}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:"// \u2193 Counts signals from callbacks without parameters\nSignalCounter signal = new();\nSignalCounter<string> signal = new();\n// \u2191 Counts signals from callbacks with a string parameter\n"})}),"\n",(0,i.jsx)(n.p,{children:"Then, you can signal the callback on the recording."}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:"class MyClass\n{\n  public void Execute(Action<string> onCompleted)\n  {\n    // do something in a background thread and then call the onCompleted callback\n  }\n}\n\nsut.Execute(v => signal.Signal(v));\n"})}),"\n",(0,i.jsx)(n.p,{children:"At last, you can wait for the callback to be signaled:"}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(signal).Should().BeSignaled();\n"})}),"\n",(0,i.jsx)(n.p,{children:"You can also verify that the callback will not be signaled:"}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(signal).Should().NotBeSignaled();\n"})}),"\n",(0,i.jsx)(n.p,{children:(0,i.jsx)(n.em,{children:"NOTE: The last statement will result never return, unless a timeout or cancellation is specified.\nTherefore, when nothing is specified, a default timeout of 30 seconds is applied!"})}),"\n",(0,i.jsx)(n.h3,{id:"timeout",children:"Timeout"}),"\n",(0,i.jsx)(n.p,{children:"You can specify a timeout, how long you want to wait for the callback to be signaled:"}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(signal).Should().BeSignaled().Within(TimeSpan.FromSeconds(5))\n  .Because("it should take at most 5 seconds to complete");\n'})}),"\n",(0,i.jsxs)(n.p,{children:["Alternatively you can also use a ",(0,i.jsx)(n.code,{children:"CancellationToken"})," for a timeout:"]}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:'CancellationToken cancellationToken = new CancellationTokenSource(5000).Token;\nawait Expect.That(signal).Should().BeSignaled().WithCancellation(cancellationToken)\n  .Because("it should be completed, before the cancellationToken is cancelled");\n'})}),"\n",(0,i.jsx)(n.h3,{id:"amount",children:"Amount"}),"\n",(0,i.jsx)(n.p,{children:"You can specify a number of times, that a callback must at least be signaled:"}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(signal).Should().BeSignaled(3.Times());\n"})}),"\n",(0,i.jsx)(n.p,{children:"You can also verify, that the callback was not signaled at least the given number of times:"}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:"await Expect.That(signal).Should().NotBeSignaled(3.Times());\n"})}),"\n",(0,i.jsx)(n.h3,{id:"parameters",children:"Parameters"}),"\n",(0,i.jsx)(n.p,{children:"You can also include a parameter during signaling:"}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:'SignalCounter<string> signal = new();\n\nsignal.Signal("foo");\nsignal.Signal("bar");\n\nawait That(signal).Should().BeSignaled(2.Times());\n'})}),"\n",(0,i.jsxs)(n.p,{children:["You can filter for signals with specific parameters by providing a ",(0,i.jsx)(n.code,{children:"predicate"}),":"]}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:'SignalCounter<string> signal = new();\n\nsignal.Signal("foo");\nsignal.Signal("bar");\nsignal.Signal("foo");\n\nawait That(signal).Should().BeSignaled(2.Times()).With(p => p == "foo");\n'})}),"\n",(0,i.jsx)(n.p,{children:(0,i.jsx)(n.em,{children:"In case of a failed expectation, the recorded parameters will be displayed in the error message."})})]})}function h(e={}){const{wrapper:n}={...(0,t.R)(),...e.components};return n?(0,i.jsx)(n,{...e,children:(0,i.jsx)(d,{...e})}):d(e)}},8453:(e,n,a)=>{a.d(n,{R:()=>l,x:()=>c});var s=a(6540);const i={},t=s.createContext(i);function l(e){const n=s.useContext(t);return s.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function c(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(i):e.components||i:l(e.components),s.createElement(t.Provider,{value:n},e.children)}}}]);