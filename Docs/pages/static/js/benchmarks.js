	(function() {
	// Colors from https://github.com/github/linguist/blob/master/lib/linguist/languages.yml
	const toolColors = {
	TestablyExpectations: '#c2185b',
	TUnit: '#00add8',
	FluentAssertions: '#178600'
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

	new Chart(canvas, {
	type: 'line',
	data,
	options,
});
}

	function renderGraphs(parent, name, datasets) {
	var div = document.createElement("div");
	var h = document.createElement("h3");
	var t = document.createTextNode(name.substring(33));
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
}),

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
	return '\n' + data.commit.message.slice(0, data.commit.message.indexOf("\n")) + '\n\n' + data.commit.timestamp + ' committed by @' + data.commit.author.username + '\n';
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

	new Chart(canvas, {
	type: 'line',
	data,
	options,
});
}

	function renderBenchSet(name, benchSet, main) {
	const setElem = document.createElement('div');
	setElem.className = 'benchmark-set';
	main.appendChild(setElem);

	const nameElem = document.createElement('h1');
	nameElem.className = 'benchmark-title';
	nameElem.textContent = name;
	setElem.appendChild(nameElem);

	const graphsElem = document.createElement('div');
	graphsElem.className = 'benchmark-graphs';
	setElem.appendChild(graphsElem);

	var chartGroups = new Map();
	for (const [benchName, benches] of benchSet.entries()) {
	var title = benchName.split("_");
	if (title.length == 2)
{
	const arr = chartGroups.get(title[0]);
	const result = [ title[1], benches ];
	if (arr === undefined) {
	chartGroups.set(title[0], [result]);
} else {
	arr.push(result);
}
}
	else
{
	renderGraph(graphsElem, benchName, benches)
}
}
	for (const [benchName, benches] of chartGroups.entries()) {
	renderGraphs(graphsElem, benchName, benches)
}
}

	const main = document.getElementById('main');
	for (const {name, dataSet} of dataSets) {
	renderBenchSet(name, dataSet, main);
}
}

	renderAllCharts(init()); // Start
})();
