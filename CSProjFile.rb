require 'rexml/document'

class CSProjFile
 def initialize(projFile)
  @projXml = REXML::Document.new projFile
  @extensions = {"library"=>"dll"}
 end

 def output_type
  @projXml.elements["VisualStudioProject/CSHARP/Build/Settings"].attributes["OutputType"].downcase
 end

 def assembly_name
  @projXml.elements["VisualStudioProject/CSHARP/Build/Settings"].attributes["AssemblyName"]
 end

 def files
  result = []
  path = "VisualStudioProject/CSHARP/Files/Include/File"
  @projXml.elements.each(path) { |element| result << element.attributes["RelPath"] if compiledFile?(element) }
  result
 end

 def compiledFile?(element)
  element.attributes["BuildAction"]=="Compile"
 end

 def embedded_resources
  result = []
  path = "VisualStudioProject/CSHARP/Files/Include/File"
  @projXml.elements.each(path) { |element| result << element.attributes["RelPath"] if embeddedResourceFile?(element) }
  result
 end

 def embeddedResourceFile?(element)
  element.attributes["BuildAction"]=="EmbeddedResource"
 end

 def references
  result = []
  @projXml.elements.each("VisualStudioProject/CSHARP/Build/References/Reference") { |element| result << element.attributes["Name"] }
  result
 end

 def create_csc(outDir)
  result = []
  result << "csc"
  result << "/out:#{outDir}/#{assembly_name}.#{@extensions[output_type]}"
  result << "/target:#{output_type}"
  result << "/lib:#{outDir}"
  refs = references.collect {|ref| ref + ".dll"}
  result << "/r:'#{refs.join(";")}'"
  embeddedFiles = embedded_resources.collect { |item| convert_to_resource(item)}
  result << embeddedFiles.join(" ")
  recurseFiles = files.each { |item| item.sub!(/.+\\/,"/recurse:") }
  result << recurseFiles.join(" ")
  result.join(" ")
 end

 def convert_to_resource(item)
  file = item.to_s
  id = "#{assembly_name}.#{item.to_s.sub(/\\/,".")}"
  "/res:'#{file},#{id}'"
 end
end

