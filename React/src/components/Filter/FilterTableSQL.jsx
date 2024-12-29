import React, { useState, useEffect } from "react";
import axios from "axios";
import toast from "react-hot-toast";
import FilterMinMax from "./FilterMinMax";

const FilterTableSQL = ({ data, page }) => {
    const [hoveredColumn, setHoveredColumn] = useState(null); // Sütun numarasını takip edecek state
    const [rowErrors, setRowErrors] = useState([]);
  
    useEffect(() => {
      const initialErrors = data.map((row) => ({
        genel_ariza: formatNumber(row.genelAriza || 0),
        mekanik_ariza: formatNumber(row.mekanikAriza || 0),
        elektrik_ariza: formatNumber(row.elektrikAriza || 0),
        isletme_ariza: formatNumber(row.isletmeAriza || 0),
      }));
      setRowErrors(initialErrors);
    }, [data]);
  
    const formatNumber = (num) => {
      return Number.isInteger(num) ? num : parseFloat(num.toFixed(1));
    };
  
    const handleErrorChange = (index, key, value,e) => {
      if (value.length > 2 && value[0] === "0") {
        e.target.value = value.slice(1); 
      }
      setRowErrors((prevErrors) => {
        const newErrors = [...prevErrors];
        newErrors[index][key] = Number(value);
        return newErrors;
      });
    };
  
    const handleUpdate = (index) => {
      const rowData = data[index];
      const errors = rowErrors[index];
      let newData = {
        ...errors,
        dataId: rowData.dataId,
      };
      axios
        .put("https://localhost:7189/api/getsqldata", newData)
        .then((res) => {
          if (res.data.success) toast.success(res.data.message);
          else toast.error(res.data.message);
        })
        .catch(() => {
          toast.error("Güncelleme başarısız!");
        });
    };

    const formatDateTime = (isoDate) => {
        const date = new Date(isoDate);
        const formattedDate = date.toLocaleDateString('tr-TR', {
          day: '2-digit',
          month: '2-digit',
          year: 'numeric',
        });
        const formattedTime = date.toLocaleTimeString('tr-TR', {
          hour: '2-digit',
          minute: '2-digit',
          second: '2-digit',
        });
        return `${formattedDate} ${formattedTime}`;
      };
  
    return (
      <div className="table-responsive pb-3 mt-4 sticky-header" style={{ maxHeight: "800px", overflowY: "auto" }}>
        <h6 className="mb-4">Toplam Satır Sayısı: {data.length}</h6>
        {<FilterMinMax data={data} />}

        <table className="filterTable">
          <thead className="text-center">
            <tr>
              <td>İŞLEM TARİHİ</td>
              {data.length > 0 &&
                JSON.parse(JSON.parse(data[data.length - 1].dataJson)).map((element, index) => (
                  <td key={`${index}-${element.data_title}`}>
                    {element.data_title}
                  </td>
              ))}
              <td>GENEL ARIZA</td>
              <td>MEKANİK ARIZA</td>
              <td>ELEKTRİK ARIZA</td>
              <td>İŞLETME ARIZA</td>
              <td>Güncelle</td>
            </tr>
          </thead>
          <tbody className="text-center">
            {data.map((row, rowIndex) => (
              <tr key={`row-${row.dataId}`}>
                <td>
                    <input type="text" value={formatDateTime(row.exportdate)} className="form-control w110" disabled readOnly/>
                </td>
                {JSON.parse(JSON.parse(row.dataJson)).map((element, key) => (
                  <td key={"sql_" + key} onMouseEnter={() => setHoveredColumn(key)} onMouseLeave={() => setHoveredColumn(null)}>
                    <input
                      type="text"
                      value={element.data_value}
                      className={`form-control ${hoveredColumn === key ? "hover" : ""} 
                        ${
                          [1, 2].includes(key) ? 'w110' : [3, 4, 5].includes(key) ? 'w70' : 'w50'
                        }
                      `}
                      disabled
                      readOnly
                    />
                  </td>
                ))}
                {["genel_ariza", "mekanik_ariza", "elektrik_ariza", "isletme_ariza"].map((key) => (
                  <td key={key}>
                    <input
                      type="number"
                      value={rowErrors[rowIndex]?.[key] || 0}
                      onChange={(e) => handleErrorChange(rowIndex, key, e.target.value,e)}
                      className="form-control ariza"
                    />
                  </td>
                ))}
                <td>
                  <button className="btn btn-primary arizabtn" onClick={() => handleUpdate(rowIndex)}>
                    Ekle
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  };
  
  export default FilterTableSQL;
  