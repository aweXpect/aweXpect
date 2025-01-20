(function () {
	const benchmarkData = window.BENCHMARK_DATA;
	function init() {
		
		const main = document.getElementById('benchmarks-container');
		for (const name in benchmarkData) {
			renderChart(main, name, benchmarkData[name]);
		}
	}

	
	function renderChart(main, name, data) {
		const div = document.createElement("div");
		const h = document.createElement("h3");
		const t = document.createTextNode(name);
		h.appendChild(t);
		div.appendChild(h);
		const canvas = document.createElement('canvas');
		canvas.className = 'benchmark-chart';
		div.appendChild(canvas);
		main.appendChild(div);

		const options = {
			responsive: true,
			interaction: {
				mode: 'index',
				intersect: false,
			},
			stacked: false,
			plugins: {
				tooltip: {
					callbacks: {
						footer: items => {
							const commitSha = items[0].label;
							const commit = data.commits.find(x => x.sha.startsWith(commitSha));
							return '\n' + commit.message + '\n' + commit.date + ' by @' + commit.author;
						},
						label: (item) =>
							`${item.formattedValue} ${item.dataset.unit} | ${item.dataset.label}`,
					},
				},
			},
			scales: {
				y: {
					type: 'linear',
					display: true,
					title: { display: true, text: 'time [ns]' },
					position: 'left',
				},
				y1: {
					type: 'linear',
					display: true,
					title: { display: true, text: 'memory [bytes]' },
					position: 'right',
					grid: {
						drawOnChartArea: false,
					},
				},
			}
		};
		new Chart(canvas, {
			type: 'line',
			data: data,
			options: options
		});
	}

	function waitForScripts() {
		if (typeof Chart !== 'undefined' && window.BENCHMARK_DATA !== 'undefined') {
			init();
		} else {
			setTimeout(waitForScripts, 100);
		}
	}

	waitForScripts();
})();
