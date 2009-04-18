require 'rubygems'
require 'rake'
require 'CSProjFile.rb'

task :default => [:compile, :test]

task :compile do
  CSProjFile.new('XStream/XStream.csproj')
end

task :test do
end
