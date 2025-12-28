import { useEffect, useState } from 'react'
import './App.css'
import axios from 'axios'

function App() {
  const [products, setProducts] = useState([])
  // Ödev olarak product tasarımlarını yap.
  // Aynı zamanda backendteki diğer endpointlerinde kullan
  const BASE_URL = 'https://localhost:7109'
  useEffect(()=>{
    axios.get(`${BASE_URL}/api/product/get-products`)
    .then((response)=>{
       if(response.status === 200){
          console.log(response.data)
          setProducts(response.data)
       }
    })
    .catch((error) => {
      console.log(error)
    })
  },[])

  return (
    <>
      <h3>Product List</h3>
      <div>
         {
          products.map((product,key)=>(
            <div key={key} className='product-box'>
                <div>
                  <p>{product.title}</p>
                  <p>{product.description}</p>
                </div>
                <div>
                  <span>{product.price}</span>
                </div>
            </div>
          ))
         }
      </div>
    </>
  )
}

export default App
