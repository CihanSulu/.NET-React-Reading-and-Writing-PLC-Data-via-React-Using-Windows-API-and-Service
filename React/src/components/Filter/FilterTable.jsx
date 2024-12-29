import React, { useState, useEffect } from 'react';
import axios from 'axios';
import toast from 'react-hot-toast';


const FilterTable = ({ data,page,resetErrors,setResetErrors }) => {
    const [hoveredColumn, setHoveredColumn] = useState(null);
    const [errors, setErrors] = useState({
        'genel_ariza': 0,
        'mekanik_ariza': 0,
        'elektrik_ariza': 0,
        'isletme_ariza': 0
    })

    const handleAddStop = ()=>{
        const newErrorEntry = { ...errors }; 
        const req = [...data]; 
        req.push(newErrorEntry); 
        axios.get(`https://localhost:7189/api/errors?page=${page}&genel=${errors.genel_ariza}&mekanik=${errors.mekanik_ariza}&elektrik=${errors.elektrik_ariza}&isletme=${errors.isletme_ariza}`).then(res=>{
            toast.success(res.data.message);
        });
    }

    const handleErrorChange = (key, value,e) => {
        if (value.length > 2 && value[0] === "0") {
            e.target.value = value.slice(1); 
        }

        setErrors((prevErrors) => ({
            ...prevErrors,
            [key]: Number(value),
        }));
    };  

    useEffect(()=>{
        axios.post('https://localhost:7189/api/errors',{page_id:page}).then(res=>{
            setErrors((prevErrors) => ({
                ...prevErrors,
                genel_ariza: res.data.genel_ariza,
                mekanik_ariza: res.data.mekanik_ariza,
                elektrik_ariza: res.data.elektrik_ariza,
                isletme_ariza: res.data.isletme_ariza,
            }));
        })
    },[page])

    useEffect(()=>{
        if(resetErrors){
            setErrors((prevErrors) => ({
                ...prevErrors,
                genel_ariza: 0,
                mekanik_ariza: 0,
                elektrik_ariza: 0,
                isletme_ariza: 0,
            }));
            setResetErrors(false)
        }
    },[resetErrors])

    return (
        <div className="table-responsive pb-3 mt-4 sticky-header" style={{ maxHeight: '800px', overflowY: 'auto' }}>

            <table className="filterTable">
                <thead className="text-center">
                    <tr>
                        {data.map((item,key)=>(
                            <td key={key}>{item.data_title}<br/><small>{item.data_description}</small></td>
                        ))}
                        {Object.keys(errors).map((key) => (
                            <td key={`error-${key}`}>{key.replace('_',' ').replaceAll('i','ı').toLocaleUpperCase()}</td> 
                        ))}
                        <td>Duraklama</td>
                    </tr>
                </thead>
                <tbody className="text-center">
                    <tr>
                        {data.map((item,i)=>(
                            <td key={i}>
                                <input
                                    type="text"
                                    value={item.data_value}
                                    className={`form-control ${
                                    hoveredColumn === 0 && 'hover'
                                    } ${
                                    [1, 2].includes(i) ? 'w110' : [3, 4, 5].includes(i) ? 'w70' : 'w50'
                                    }`}
                                    disabled
                                    readOnly
                                />
                            </td>
                        ))}
                        {Object.keys(errors).map(key => (
                            <td key={key}>
                                <input type="number" value={errors[key]} onChange={(e) => handleErrorChange(key, e.target.value,e)} className="form-control ariza" />
                            </td>
                        ))}
                        <td>
                            <button className="btn btn-primary arizabtn" onClick={() => handleAddStop()}>Ekle</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
};

export default FilterTable;
