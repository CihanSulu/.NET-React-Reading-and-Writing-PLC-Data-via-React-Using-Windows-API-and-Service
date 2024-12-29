import React from 'react';

// Başlıkları formatlamak için bir yardımcı fonksiyon
const formatHeader = (key) => {
  if (key === 'genelAriza') return 'GENEL ARIZA';
  if (key === 'mekanikAriza') return 'MEKANİK ARIZA';
  if (key === 'elektrikAriza') return 'ELEKTRİK ARIZA';
  if (key === 'isletmeAriza') return 'İŞLETME ARIZA';
  return key
    .replace(/_/g, ' ')
    .replace(/\b\w/g, (char) => char.toUpperCase());
};

const calculateMinMaxAvg = (data, key) => {
  const values = data.map((item) => parseFloat(item[key]) || 0);
  const min = Math.min(...values);
  const max = Math.max(...values);
  const avg = values.reduce((sum, val) => sum + val, 0) / values.length;

  const formatValue = (value) => (value % 1 === 0 ? value : parseFloat(value.toFixed(1)));

  return {
    min: formatValue(min),
    max: formatValue(max),
    avg: formatValue(avg),
  };
};

const FilterMinMax = ({ data }) => {
  // dataJson içerisindeki başlıkları ve değerleri çıkar
  const parsedData = data.map((item) => ({
    ...item,
    dataJson: JSON.parse(JSON.parse(item.dataJson)),
  }));

  // data_title başlıklarını al
  const dataTitles = parsedData[0]?.dataJson.map((entry) => entry.data_title) || [];

  // Arıza kayıtlarının başlıklarını belirle
  const arizaKeys = ['genelAriza', 'mekanikAriza', 'elektrikAriza', 'isletmeAriza'];

  // Tüm başlıkları birleştir
  const allHeaders = [...dataTitles, ...arizaKeys];

  // Min, Max, Avg hesaplamalarını yap
  const dataCalculations = {};
  dataTitles.forEach((title) => {
    const values = parsedData.map(
      (item) =>
        item.dataJson.find((entry) => entry.data_title === title)?.data_value || 0
    );
    dataCalculations[title] = calculateMinMaxAvg(values.map((v) => ({ value: v })), 'value');
  });

  const arizaCalculations = {};
  arizaKeys.forEach((key) => {
    arizaCalculations[key] = calculateMinMaxAvg(data, key);
  });

  return (
    <div
      className="table-responsive2 pb-3 mt-4 sticky-header"
      style={{ maxHeight: '600px' }}
    >
      <table className="filterTable">
        <thead className="text-center">
          <tr>
            <th style={{fontSize:"9px"}}>Değerler</th>
            {allHeaders.map((header, index) => (
              <th className={`${(index == 0  || index == 1 || index == 2) && 'd-none'}`} style={{fontSize:"9px"}} key={index}>{formatHeader(header)}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {['min', 'max', 'avg'].map((stat) => (
            <tr key={stat}>
              <td>
                <input
                  type="text"
                  value={stat.toUpperCase()}
                  className="form-control bg-primary2 text-white"
                  style={{width:"390px",fontSize:"9px"}}
                  disabled
                  readOnly
                />
              </td>
              {dataTitles.map((title, index) => (
                <td key={index} className={`${(index == 0  || index == 1 || index == 2) && 'd-none'}`}>
                  <input
                    type="number"
                    value={dataCalculations[title][stat]}
                    className={`form-control ${[1, 2].includes(index) ? 'w110' : [3, 4, 5].includes(index) ? 'w70' : 'w50'}`}
                    disabled
                    readOnly
                  />
                </td>
              ))}
              {arizaKeys.map((key, index) => (
                <td key={`ariza-${index}`}>
                  <input
                    type="number"
                    value={arizaCalculations[key][stat]}
                    className="form-control ariza"
                    disabled
                    readOnly
                  />
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default FilterMinMax;
