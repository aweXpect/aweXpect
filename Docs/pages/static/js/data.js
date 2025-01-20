window.BENCHMARK_DATA = {
  "Bool": {
    "commits": [
      {
        "sha": "198447f7ad33650ea18d7239d6579cda44f17f2f",
        "author": "Valentin Breu\u00DF",
        "date": "Sun Jan 19 22:32:18 2025 \u002B0100",
        "message": "fix: execute benchmark report in main build (#219)"
      },
      {
        "sha": "e994b4ddac319ba1496d5d908adabef71217f6b5",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:06:43 2025 \u002B0100",
        "message": "docs: avoid reporting benchmarks for the same commit twice (#221)"
      },
      {
        "sha": "a6022c2c3716943fc039096336119983cf150e7e",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:17:07 2025 \u002B0100",
        "message": "coverage: exclude polyfill files (#222)"
      },
      {
        "sha": "ba348350c55bc5b042f1e6116e5f4ddb00a80a24",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:43:03 2025 \u002B0100",
        "message": "fix: finalize release when at least one push succeeded (#223)"
      },
      {
        "sha": "96f70d37e5bf44488ff777dab44333c47fbccfeb",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 11:28:18 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0 (#229)"
      },
      {
        "sha": "a65ca3f33d06e0a712049c084956217791a1af8d",
        "author": "Valentin",
        "date": "Mon Jan 20 11:18:44 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0"
      },
      {
        "sha": "b6626f8ea71062e77ed99d409fffa363faeeb86c",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:26 2025 \u002B0100",
        "message": "build(deps): bump Microsoft.Testing.Extensions.TrxReport from 1.5.0 to 1.5.1 (#228)"
      },
      {
        "sha": "08ef69d9676332d2cc040e885cf1d859ae9a0238",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:36 2025 \u002B0100",
        "message": "build(deps): bump SharpCompress from 0.38.0 to 0.39.0 (#227)"
      },
      {
        "sha": "8c5638ae5f225aa69698100f545da758885f3385",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:43 2025 \u002B0100",
        "message": "build(deps): bump TUnit.Assertions from 0.6.137 to 0.6.151 in the tunit group (#226)"
      },
      {
        "sha": "8111de1379be71399916ded9e459e43d9a746ca3",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:53 2025 \u002B0100",
        "message": "build(deps): bump the mstest group with 2 updates (#225)"
      }
    ],
    "labels": [
      "198447f7",
      "e994b4dd",
      "a6022c2c",
      "ba348350",
      "96f70d37",
      "a65ca3f3",
      "b6626f8e",
      "08ef69d9",
      "8c5638ae",
      "8111de13"
    ],
    "datasets": [
      {
        "label": "aweXpect time",
        "unit": "ns",
        "data": [
          150.75262652910672,
          170.8307419459025,
          170.6940963904063,
          158.4435460090637,
          155.13557620048522,
          165.76554095745087,
          164.67326958974203,
          160.11295573527997,
          156.0363313992818,
          151.6461879014969
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "aweXpect memory",
        "unit": "b",
        "data": [
          496,
          496,
          496,
          496,
          496,
          496,
          496,
          496,
          496,
          496
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "FluentAssertions time",
        "unit": "ns",
        "data": [
          200.27242479721704,
          224.86235439777374,
          222.18419427871703,
          199.29969428135797,
          207.76855233510335,
          225.15658162434895,
          208.41571418444315,
          206.88912995656332,
          202.32963379224142,
          205.74112950052535
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "FluentAssertions memory",
        "unit": "b",
        "data": [
          776,
          776,
          776,
          776,
          776,
          776,
          776,
          776,
          776,
          776
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "TUnit time",
        "unit": "ns",
        "data": [
          539.4753929138184,
          595.9496761322022,
          578.6159603754679,
          524.8989543233599,
          562.5271207491556,
          567.452426846822,
          550.8775363922119,
          522.3786952836173,
          549.5231369654338,
          568.7060990651448
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "TUnit memory",
        "unit": "b",
        "data": [
          1712,
          1712,
          1712,
          1712,
          1712,
          1712,
          1712,
          1712,
          1752,
          1752
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      }
    ]
  },
  "ItemsCount_AtLeast": {
    "commits": [
      {
        "sha": "198447f7ad33650ea18d7239d6579cda44f17f2f",
        "author": "Valentin Breu\u00DF",
        "date": "Sun Jan 19 22:32:18 2025 \u002B0100",
        "message": "fix: execute benchmark report in main build (#219)"
      },
      {
        "sha": "e994b4ddac319ba1496d5d908adabef71217f6b5",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:06:43 2025 \u002B0100",
        "message": "docs: avoid reporting benchmarks for the same commit twice (#221)"
      },
      {
        "sha": "a6022c2c3716943fc039096336119983cf150e7e",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:17:07 2025 \u002B0100",
        "message": "coverage: exclude polyfill files (#222)"
      },
      {
        "sha": "ba348350c55bc5b042f1e6116e5f4ddb00a80a24",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:43:03 2025 \u002B0100",
        "message": "fix: finalize release when at least one push succeeded (#223)"
      },
      {
        "sha": "96f70d37e5bf44488ff777dab44333c47fbccfeb",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 11:28:18 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0 (#229)"
      },
      {
        "sha": "a65ca3f33d06e0a712049c084956217791a1af8d",
        "author": "Valentin",
        "date": "Mon Jan 20 11:18:44 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0"
      },
      {
        "sha": "b6626f8ea71062e77ed99d409fffa363faeeb86c",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:26 2025 \u002B0100",
        "message": "build(deps): bump Microsoft.Testing.Extensions.TrxReport from 1.5.0 to 1.5.1 (#228)"
      },
      {
        "sha": "08ef69d9676332d2cc040e885cf1d859ae9a0238",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:36 2025 \u002B0100",
        "message": "build(deps): bump SharpCompress from 0.38.0 to 0.39.0 (#227)"
      },
      {
        "sha": "8c5638ae5f225aa69698100f545da758885f3385",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:43 2025 \u002B0100",
        "message": "build(deps): bump TUnit.Assertions from 0.6.137 to 0.6.151 in the tunit group (#226)"
      },
      {
        "sha": "8111de1379be71399916ded9e459e43d9a746ca3",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:53 2025 \u002B0100",
        "message": "build(deps): bump the mstest group with 2 updates (#225)"
      }
    ],
    "labels": [
      "198447f7",
      "e994b4dd",
      "a6022c2c",
      "ba348350",
      "96f70d37",
      "a65ca3f3",
      "b6626f8e",
      "08ef69d9",
      "8c5638ae",
      "8111de13"
    ],
    "datasets": [
      {
        "label": "aweXpect time",
        "unit": "ns",
        "data": [
          329.63557247320813,
          351.37352555592855,
          374.3091664632162,
          337.2535416603088,
          352.39308126156146,
          381.1330859502157,
          336.44947052001953,
          365.3181669371469,
          345.05609801610314,
          352.852764742715
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "aweXpect memory",
        "unit": "b",
        "data": [
          888,
          888,
          888,
          888,
          888,
          888,
          888,
          888,
          888,
          888
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "FluentAssertions time",
        "unit": "ns",
        "data": [
          417.0791400029109,
          460.97408453623456,
          456.0185529504503,
          406.35335045594434,
          436.15411609013876,
          457.1187669436137,
          424.73906723658246,
          442.4963041305542,
          412.4149733543396,
          473.0607053756714
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "FluentAssertions memory",
        "unit": "b",
        "data": [
          1816,
          1816,
          1816,
          1816,
          1816,
          1816,
          1816,
          1816,
          1816,
          1816
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "TUnit time",
        "unit": "ns",
        "data": [
          14911.549033610027,
          15581.482432047525,
          14722.414560953775,
          14283.944193913387,
          22458.590706380208,
          14792.756424967449,
          15938.436789879432,
          20583.53896077474,
          14059.436920166016,
          17242.750053992637
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "TUnit memory",
        "unit": "b",
        "data": [
          26616,
          26616,
          26616,
          26616,
          26616,
          26616,
          26616,
          26616,
          26552,
          26552
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      }
    ]
  },
  "Int_GreaterThan": {
    "commits": [
      {
        "sha": "198447f7ad33650ea18d7239d6579cda44f17f2f",
        "author": "Valentin Breu\u00DF",
        "date": "Sun Jan 19 22:32:18 2025 \u002B0100",
        "message": "fix: execute benchmark report in main build (#219)"
      },
      {
        "sha": "e994b4ddac319ba1496d5d908adabef71217f6b5",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:06:43 2025 \u002B0100",
        "message": "docs: avoid reporting benchmarks for the same commit twice (#221)"
      },
      {
        "sha": "a6022c2c3716943fc039096336119983cf150e7e",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:17:07 2025 \u002B0100",
        "message": "coverage: exclude polyfill files (#222)"
      },
      {
        "sha": "ba348350c55bc5b042f1e6116e5f4ddb00a80a24",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:43:03 2025 \u002B0100",
        "message": "fix: finalize release when at least one push succeeded (#223)"
      },
      {
        "sha": "96f70d37e5bf44488ff777dab44333c47fbccfeb",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 11:28:18 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0 (#229)"
      },
      {
        "sha": "a65ca3f33d06e0a712049c084956217791a1af8d",
        "author": "Valentin",
        "date": "Mon Jan 20 11:18:44 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0"
      },
      {
        "sha": "b6626f8ea71062e77ed99d409fffa363faeeb86c",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:26 2025 \u002B0100",
        "message": "build(deps): bump Microsoft.Testing.Extensions.TrxReport from 1.5.0 to 1.5.1 (#228)"
      },
      {
        "sha": "08ef69d9676332d2cc040e885cf1d859ae9a0238",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:36 2025 \u002B0100",
        "message": "build(deps): bump SharpCompress from 0.38.0 to 0.39.0 (#227)"
      },
      {
        "sha": "8c5638ae5f225aa69698100f545da758885f3385",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:43 2025 \u002B0100",
        "message": "build(deps): bump TUnit.Assertions from 0.6.137 to 0.6.151 in the tunit group (#226)"
      },
      {
        "sha": "8111de1379be71399916ded9e459e43d9a746ca3",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:53 2025 \u002B0100",
        "message": "build(deps): bump the mstest group with 2 updates (#225)"
      }
    ],
    "labels": [
      "198447f7",
      "e994b4dd",
      "a6022c2c",
      "ba348350",
      "96f70d37",
      "a65ca3f3",
      "b6626f8e",
      "08ef69d9",
      "8c5638ae",
      "8111de13"
    ],
    "datasets": [
      {
        "label": "aweXpect time",
        "unit": "ns",
        "data": [
          204.06733180681866,
          216.9594315801348,
          206.10692450205485,
          196.2139249581557,
          206.1067977587382,
          214.8942221959432,
          199.87026483217875,
          201.19402921994526,
          192.24772651378925,
          200.14843589918954
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "aweXpect memory",
        "unit": "b",
        "data": [
          848,
          848,
          848,
          848,
          848,
          848,
          848,
          848,
          848,
          848
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "FluentAssertions time",
        "unit": "ns",
        "data": [
          250.96880504063196,
          278.63078991572064,
          270.1289446512858,
          242.58121951421103,
          274.3820020675659,
          270.8007615974971,
          264.8141753514608,
          270.9082823912303,
          248.62949003492082,
          267.79543792284454
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "FluentAssertions memory",
        "unit": "b",
        "data": [
          1048,
          1048,
          1048,
          1048,
          1048,
          1048,
          1048,
          1048,
          1048,
          1048
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "TUnit time",
        "unit": "ns",
        "data": [
          766.1536415735881,
          829.1234278996785,
          821.9613706588746,
          849.6050724983215,
          797.4266170092991,
          815.1987977981568,
          817.5118516286215,
          793.0287864685058,
          813.043058013916,
          841.1511103947958
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "TUnit memory",
        "unit": "b",
        "data": [
          2352,
          2352,
          2352,
          2352,
          2352,
          2352,
          2352,
          2352,
          2240,
          2240
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      }
    ]
  },
  "String": {
    "commits": [
      {
        "sha": "198447f7ad33650ea18d7239d6579cda44f17f2f",
        "author": "Valentin Breu\u00DF",
        "date": "Sun Jan 19 22:32:18 2025 \u002B0100",
        "message": "fix: execute benchmark report in main build (#219)"
      },
      {
        "sha": "e994b4ddac319ba1496d5d908adabef71217f6b5",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:06:43 2025 \u002B0100",
        "message": "docs: avoid reporting benchmarks for the same commit twice (#221)"
      },
      {
        "sha": "a6022c2c3716943fc039096336119983cf150e7e",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:17:07 2025 \u002B0100",
        "message": "coverage: exclude polyfill files (#222)"
      },
      {
        "sha": "ba348350c55bc5b042f1e6116e5f4ddb00a80a24",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:43:03 2025 \u002B0100",
        "message": "fix: finalize release when at least one push succeeded (#223)"
      },
      {
        "sha": "96f70d37e5bf44488ff777dab44333c47fbccfeb",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 11:28:18 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0 (#229)"
      },
      {
        "sha": "a65ca3f33d06e0a712049c084956217791a1af8d",
        "author": "Valentin",
        "date": "Mon Jan 20 11:18:44 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0"
      },
      {
        "sha": "b6626f8ea71062e77ed99d409fffa363faeeb86c",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:26 2025 \u002B0100",
        "message": "build(deps): bump Microsoft.Testing.Extensions.TrxReport from 1.5.0 to 1.5.1 (#228)"
      },
      {
        "sha": "08ef69d9676332d2cc040e885cf1d859ae9a0238",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:36 2025 \u002B0100",
        "message": "build(deps): bump SharpCompress from 0.38.0 to 0.39.0 (#227)"
      },
      {
        "sha": "8c5638ae5f225aa69698100f545da758885f3385",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:43 2025 \u002B0100",
        "message": "build(deps): bump TUnit.Assertions from 0.6.137 to 0.6.151 in the tunit group (#226)"
      },
      {
        "sha": "8111de1379be71399916ded9e459e43d9a746ca3",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:53 2025 \u002B0100",
        "message": "build(deps): bump the mstest group with 2 updates (#225)"
      }
    ],
    "labels": [
      "198447f7",
      "e994b4dd",
      "a6022c2c",
      "ba348350",
      "96f70d37",
      "a65ca3f3",
      "b6626f8e",
      "08ef69d9",
      "8c5638ae",
      "8111de13"
    ],
    "datasets": [
      {
        "label": "aweXpect time",
        "unit": "ns",
        "data": [
          304.53645614477307,
          328.66932388714383,
          306.69245314598083,
          301.3085208960942,
          315.39779373577665,
          317.1949115753174,
          301.54692827860515,
          315.0062762260437,
          307.0068321961623,
          314.97997426986694
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "aweXpect memory",
        "unit": "b",
        "data": [
          928,
          928,
          928,
          928,
          928,
          928,
          928,
          928,
          928,
          928
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "FluentAssertions time",
        "unit": "ns",
        "data": [
          351.26639202662875,
          409.45268201828003,
          351.31532487869265,
          354.7790416399638,
          364.92660064697264,
          375.06243960062665,
          370.2074193954468,
          360.54308700561523,
          347.67961702346804,
          368.80354767579297
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "FluentAssertions memory",
        "unit": "b",
        "data": [
          1832,
          1832,
          1832,
          1832,
          1832,
          1832,
          1832,
          1832,
          1832,
          1832
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "TUnit time",
        "unit": "ns",
        "data": [
          814.6544300715128,
          895.92631987163,
          855.6375809351604,
          903.2392115910848,
          844.5590480804443,
          825.1556023279826,
          838.7160714956431,
          807.5309039524624,
          950.0381783167521,
          942.2038411458333
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "TUnit memory",
        "unit": "b",
        "data": [
          2328,
          2328,
          2328,
          2328,
          2328,
          2328,
          2328,
          2328,
          2232,
          2232
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      }
    ]
  },
  "StringArray": {
    "commits": [
      {
        "sha": "198447f7ad33650ea18d7239d6579cda44f17f2f",
        "author": "Valentin Breu\u00DF",
        "date": "Sun Jan 19 22:32:18 2025 \u002B0100",
        "message": "fix: execute benchmark report in main build (#219)"
      },
      {
        "sha": "e994b4ddac319ba1496d5d908adabef71217f6b5",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:06:43 2025 \u002B0100",
        "message": "docs: avoid reporting benchmarks for the same commit twice (#221)"
      },
      {
        "sha": "a6022c2c3716943fc039096336119983cf150e7e",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:17:07 2025 \u002B0100",
        "message": "coverage: exclude polyfill files (#222)"
      },
      {
        "sha": "ba348350c55bc5b042f1e6116e5f4ddb00a80a24",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:43:03 2025 \u002B0100",
        "message": "fix: finalize release when at least one push succeeded (#223)"
      },
      {
        "sha": "96f70d37e5bf44488ff777dab44333c47fbccfeb",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 11:28:18 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0 (#229)"
      },
      {
        "sha": "a65ca3f33d06e0a712049c084956217791a1af8d",
        "author": "Valentin",
        "date": "Mon Jan 20 11:18:44 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0"
      },
      {
        "sha": "b6626f8ea71062e77ed99d409fffa363faeeb86c",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:26 2025 \u002B0100",
        "message": "build(deps): bump Microsoft.Testing.Extensions.TrxReport from 1.5.0 to 1.5.1 (#228)"
      },
      {
        "sha": "08ef69d9676332d2cc040e885cf1d859ae9a0238",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:36 2025 \u002B0100",
        "message": "build(deps): bump SharpCompress from 0.38.0 to 0.39.0 (#227)"
      },
      {
        "sha": "8c5638ae5f225aa69698100f545da758885f3385",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:43 2025 \u002B0100",
        "message": "build(deps): bump TUnit.Assertions from 0.6.137 to 0.6.151 in the tunit group (#226)"
      },
      {
        "sha": "8111de1379be71399916ded9e459e43d9a746ca3",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:53 2025 \u002B0100",
        "message": "build(deps): bump the mstest group with 2 updates (#225)"
      }
    ],
    "labels": [
      "198447f7",
      "e994b4dd",
      "a6022c2c",
      "ba348350",
      "96f70d37",
      "a65ca3f3",
      "b6626f8e",
      "08ef69d9",
      "8c5638ae",
      "8111de13"
    ],
    "datasets": [
      {
        "label": "aweXpect time",
        "unit": "ns",
        "data": [
          1069.4012530190605,
          1128.346786226545,
          1098.832622391837,
          1076.532111985343,
          1181.8951923370362,
          1101.8187058766682,
          1068.2992315292358,
          1067.380584335327,
          1056.3488892873129,
          1062.4824607031685
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "aweXpect memory",
        "unit": "b",
        "data": [
          2384,
          2384,
          2384,
          2384,
          2384,
          2384,
          2384,
          2384,
          2384,
          2384
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "FluentAssertions time",
        "unit": "ns",
        "data": [
          1175.168862915039,
          1306.7912724812825,
          1294.6244603474936,
          1156.1905431111654,
          1206.3019789377847,
          1235.2764401753743,
          1188.1393574305944,
          1209.3633190155028,
          1204.6632853190104,
          1177.385548655192
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "FluentAssertions memory",
        "unit": "b",
        "data": [
          3760,
          3760,
          3760,
          3760,
          3760,
          3760,
          3760,
          3760,
          3760,
          3760
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "TUnit time",
        "unit": "ns",
        "data": [
          2060.838473510742,
          2126.9997182210286,
          2178.2397992060733,
          2115.8911359151202,
          2086.3043131510417,
          2090.007182820638,
          2170.923581804548,
          2082.4139742533366,
          2234.402118174235,
          2267.74205163809
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "TUnit memory",
        "unit": "b",
        "data": [
          3216,
          3216,
          3216,
          3216,
          3216,
          3216,
          3216,
          3216,
          3256,
          3256
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      }
    ]
  },
  "StringArrayInAnyOrder": {
    "commits": [
      {
        "sha": "198447f7ad33650ea18d7239d6579cda44f17f2f",
        "author": "Valentin Breu\u00DF",
        "date": "Sun Jan 19 22:32:18 2025 \u002B0100",
        "message": "fix: execute benchmark report in main build (#219)"
      },
      {
        "sha": "e994b4ddac319ba1496d5d908adabef71217f6b5",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:06:43 2025 \u002B0100",
        "message": "docs: avoid reporting benchmarks for the same commit twice (#221)"
      },
      {
        "sha": "a6022c2c3716943fc039096336119983cf150e7e",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:17:07 2025 \u002B0100",
        "message": "coverage: exclude polyfill files (#222)"
      },
      {
        "sha": "ba348350c55bc5b042f1e6116e5f4ddb00a80a24",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 08:43:03 2025 \u002B0100",
        "message": "fix: finalize release when at least one push succeeded (#223)"
      },
      {
        "sha": "96f70d37e5bf44488ff777dab44333c47fbccfeb",
        "author": "Valentin Breu\u00DF",
        "date": "Mon Jan 20 11:28:18 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0 (#229)"
      },
      {
        "sha": "a65ca3f33d06e0a712049c084956217791a1af8d",
        "author": "Valentin",
        "date": "Mon Jan 20 11:18:44 2025 \u002B0100",
        "message": "chore(deps): update aweXpect.Core to v0.18.0"
      },
      {
        "sha": "b6626f8ea71062e77ed99d409fffa363faeeb86c",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:26 2025 \u002B0100",
        "message": "build(deps): bump Microsoft.Testing.Extensions.TrxReport from 1.5.0 to 1.5.1 (#228)"
      },
      {
        "sha": "08ef69d9676332d2cc040e885cf1d859ae9a0238",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:36 2025 \u002B0100",
        "message": "build(deps): bump SharpCompress from 0.38.0 to 0.39.0 (#227)"
      },
      {
        "sha": "8c5638ae5f225aa69698100f545da758885f3385",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:43 2025 \u002B0100",
        "message": "build(deps): bump TUnit.Assertions from 0.6.137 to 0.6.151 in the tunit group (#226)"
      },
      {
        "sha": "8111de1379be71399916ded9e459e43d9a746ca3",
        "author": "dependabot[bot]",
        "date": "Mon Jan 20 11:54:53 2025 \u002B0100",
        "message": "build(deps): bump the mstest group with 2 updates (#225)"
      }
    ],
    "labels": [
      "198447f7",
      "e994b4dd",
      "a6022c2c",
      "ba348350",
      "96f70d37",
      "a65ca3f3",
      "b6626f8e",
      "08ef69d9",
      "8c5638ae",
      "8111de13"
    ],
    "datasets": [
      {
        "label": "aweXpect time",
        "unit": "ns",
        "data": [
          1094.4609926768712,
          1179.811538441976,
          1194.3902856191,
          1128.3401187016414,
          1133.359532220023,
          1140.0626906077066,
          1114.837188584464,
          1105.9909159342449,
          1085.2066505432128,
          1121.4894528706868
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "aweXpect memory",
        "unit": "b",
        "data": [
          2408,
          2408,
          2408,
          2408,
          2408,
          2408,
          2408,
          2408,
          2408,
          2408
        ],
        "borderColor": "#63A2AC",
        "backgroundColor": "#63A2AC",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "FluentAssertions time",
        "unit": "ns",
        "data": [
          76939.67994791667,
          82148.39580829327,
          82674.91611328124,
          78198.26883370536,
          79701.94614955357,
          78199.53501674107,
          79218.87158203125,
          78894.54677734376,
          77662.87524414062,
          77152.24766322544
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "FluentAssertions memory",
        "unit": "b",
        "data": [
          50710,
          50710,
          50710,
          50710,
          50710,
          50710,
          50710,
          50710,
          50710,
          50710
        ],
        "borderColor": "#ACA263",
        "backgroundColor": "#ACA263",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      },
      {
        "label": "TUnit time",
        "unit": "ns",
        "data": [
          3975.66723429362,
          4182.777239118303,
          4299.858325703939,
          4055.510234069824,
          4066.8915339152018,
          4005.5797353108724,
          4133.923426114596,
          4168.885194905599,
          4045.5662724812823,
          4230.382392883301
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y",
        "borderDash": [],
        "pointStyle": "circle"
      },
      {
        "label": "TUnit memory",
        "unit": "b",
        "data": [
          5160,
          5160,
          5160,
          5160,
          5160,
          5160,
          5160,
          5160,
          5128,
          5128
        ],
        "borderColor": "#AC6262",
        "backgroundColor": "#AC6262",
        "yAxisID": "y1",
        "borderDash": [
          5,
          5
        ],
        "pointStyle": "triangle"
      }
    ]
  }
}