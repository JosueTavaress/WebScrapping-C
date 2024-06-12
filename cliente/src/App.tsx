import CRoutes from "./routes";
import { ChakraProvider } from '@chakra-ui/react'

function App() {
  return (
    <ChakraProvider>
      <CRoutes />
    </ChakraProvider>
  )
}

export default App;