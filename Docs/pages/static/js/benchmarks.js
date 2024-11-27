(function () {
	const toolColors = {
		aweXpect: '#63A2AC',
		FluentAssertions: '#ACA263',
		TUnit: '#AC6262',
	};

	function init() {
		function collectBenchesPerTestCase(entries) {
			const map = new Map();
			for (const entry of entries) {
				const {commit, date, tool, benches} = entry;
				for (const bench of benches) {
					const result = {commit, date, tool, bench};
					const arr = map.get(bench.name);
					if (arr === undefined) {
						map.set(bench.name, [result]);
					} else {
						arr.push(result);
					}
				}
			}
			return map;
		}

		const data = window.BENCHMARK_DATA;

		// Prepare data points for charts
		return Object.keys(data.entries).map(name => ({
			name,
			dataSet: collectBenchesPerTestCase(data.entries[name]),
		}));
	}

	function renderAllCharts(dataSets) {

		function renderGraph(parent, name, dataset) {
			const canvas = document.createElement('canvas');
			canvas.className = 'benchmark-chart';
			parent.appendChild(canvas);

			const color = toolColors[dataset.length > 0 ? dataset[0].tool : '_'];
			const data = {
				labels: dataset.map(d => d.commit.id.slice(0, 7)),
				datasets: [
					{
						label: name,
						data: dataset.map(d => d.bench.value),
						borderColor: color,
						backgroundColor: color + '60', // Add alpha for #rrggbbaa
					}
				],
			};
			const options = {
				scales: {
					xAxes: [
						{
							scaleLabel: {
								display: true,
								labelString: 'commit',
							},
						}
					],
					yAxes: [
						{
							scaleLabel: {
								display: true,
								labelString: dataset.length > 0 ? dataset[0].bench.unit : '',
							},
							ticks: {
								beginAtZero: true,
							}
						}
					],
				},
				tooltips: {
					callbacks: {
						afterTitle: items => {
							const {index} = items[0];
							const data = dataset[index];
							return '\n' + data.commit.message.slice(0, data.commit.message.indexOf("\n")) + '\n\n' + data.commit.timestamp + ' committed by @' + data.commit.author.username + '\n';
						},
						label: item => {
							let label = item.value;
							const {range, unit} = dataset[item.index].bench;
							label += ' ' + unit;
							if (range) {
								label += ' (' + range + ')';
							}
							return label;
						},
						afterLabel: item => {
							const {extra} = dataset[item.index].bench;
							return extra ? '\n' + extra : '';
						}
					}
				},
				onClick: (_mouseEvent, activeElems) => {
					if (activeElems.length === 0) {
						return;
					}
					// XXX: Undocumented. How can we know the index?
					const index = activeElems[0]._index;
					const url = dataset[index].commit.url;
					window.open(url, '_blank');
				},
			};

			return new Chart(canvas, {
				type: 'line',
				data,
				options,
			});
		}

		function renderGraphs(parent, name, datasets) {
			const div = document.createElement("div");
			const h = document.createElement("h3");
			const t = document.createTextNode(name.substring(20));
			h.appendChild(t);
			div.appendChild(h);
			const canvas = document.createElement('canvas');
			canvas.className = 'benchmark-chart';
			div.appendChild(canvas);
			parent.appendChild(div);

			const data = {
				labels: datasets[0][1].map(d => d.commit.id.slice(0, 7)),
				datasets: datasets.map((value, key) => {
					const color = toolColors[value[0]];
					return {
						label: value[0],
						data: value[1].map(d => d.bench.value),
						borderColor: color,
						backgroundColor: color + '60', // Add alpha for #rrggbbaa
					};
				}).sort((a, b) => a.label.localeCompare(b.label)),

			};
			const options = {
				scales: {
					xAxes: [
						{
							scaleLabel: {
								display: true,
								labelString: 'commit',
							},
						}
					],
					yAxes: [
						{
							scaleLabel: {
								display: true,
								labelString: datasets[0][1].length > 0 ? datasets[0][1][0].bench.unit : '',
							},
							ticks: {
								beginAtZero: true,
							}
						}
					],
				},
				tooltips: {
					callbacks: {
						afterTitle: items => {
							const {index} = items[0];
							const data = datasets[0][1][index];
							return '\n' + data.commit.message.split('\n')[0] + '\n\n' + data.commit.timestamp + ' committed by @' + data.commit.author.username + '\n';
						},
						label: item => {
							let label = item.value;
							const {range, unit} = datasets[0][1][item.index].bench;
							label += ' ' + unit;
							if (range) {
								label += ' (' + range + ')';
							}
							return label;
						},
						afterLabel: item => {
							const {extra} = datasets[0][1][item.index].bench;
							return extra ? '\n' + extra : '';
						}
					}
				},
				onClick: (_mouseEvent, activeElems) => {
					if (activeElems.length === 0) {
						return;
					}
					// XXX: Undocumented. How can we know the index?
					const index = activeElems[0]._index;
					const url = datasets[0][1][index].commit.url;
					window.open(url, '_blank');
				},
			};

			return new Chart(canvas, {
				type: 'line',
				data,
				options,
			});
		}

		function renderBenchSet(benchSet, main) {
			const chartGroups = new Map();
			for (const [benchName, benches] of benchSet.entries()) {
				const title = benchName.split("_");
				if (title.length == 2) {
					const arr = chartGroups.get(title[0]);
					const result = [title[1], benches];
					if (arr === undefined) {
						chartGroups.set(title[0], [result]);
					} else {
						arr.push(result);
					}
				} else {
					renderGraph(main, benchName, benches)
				}
			}
			for (const [benchName, benches] of chartGroups.entries()) {
				renderGraphs(main, benchName, benches)
			}
		}

		const main = document.getElementById('benchmarks-container');
		for (const {name, dataSet} of dataSets) {
			renderBenchSet(dataSet, main);
		}
	}

	function waitForScripts(){
		if (typeof Chart !== 'undefined' && window.BENCHMARK_DATA !== 'undefined') {
			renderAllCharts(init());
		} else {
			setTimeout(waitForScripts, 100);
		}
	}
	waitForScripts();
})();
