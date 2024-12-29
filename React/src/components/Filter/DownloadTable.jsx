import React from 'react'
import { FaFileExcel } from "react-icons/fa";
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

const DownloadTable = ({data}) => {
    
    const exportToExcel = () => {
        if (!data || data.length === 0) return;
    
        // İlk elemandan başlıkları al
        const firstItem = JSON.parse(JSON.parse(data[0].dataJson));
        const headers = firstItem.map((item) => item.data_title);
    
        // Genel arıza, mekanik arıza vb. başlıkları ekle
        headers.push('Genel Arıza', 'Mekanik Arıza', 'Elektrik Arıza', 'İşletme Arıza');
    
        // Verileri işlemeye başla
        const rows = data.map((item) => {
          const parsedDataJson = JSON.parse(JSON.parse(item.dataJson));
    
          // data_value'ları satır verisine ekle
          const row = parsedDataJson.map((entry) => entry.data_value);
    
          // Ek sütün verilerini ekle
          row.push(item.genelAriza, item.mekanikAriza, item.elektrikAriza, item.isletmeAriza);
    
          return row;
        });
    
        // İlk satır başlıklar olacak şekilde tablonun tamamını hazırla
        const worksheetData = [headers, ...rows];
    
        // Excel dosyasını oluştur
        const worksheet = XLSX.utils.aoa_to_sheet(worksheetData);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, 'Data Export');
    
        // Dosyayı indir
        const currentDate = new Date();
        const formattedDate = `${currentDate.getDate().toString().padStart(2, '0')}.${(currentDate.getMonth() + 1).toString().padStart(2, '0')}.${currentDate.getFullYear()}`;
        
        const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
        saveAs(blob, `TIA-Datas-${formattedDate}.xlsx`);
      };
    
    return (
        <>
            {data.length != 0 && <button className='btn btn-primary' onClick={exportToExcel} style={{width:"160px"}}><FaFileExcel /> Excel Çıktısı Al</button>}
        </>
    )
}

export default DownloadTable