#region License

/*
 * Copyright � 2002-2011 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

#region Imports

using NUnit.Framework;
using Spring.Objects.Factory.Support;
using System.Collections;
using Spring.Core.TypeConversion;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#endregion

namespace Spring.Objects.Factory.Xml
{
	/// <summary>
	/// Unit and integration tests for collection conversion support
	/// </summary>
	/// <author>Choy Rim</author>
	[TestFixture]
    [Description("SPRNET-1470 Setting property of type IList<T> using <list/> without the @element-type specified fails.")]
	public class CollectionConversionGenericTests
	{
	    private DefaultListableObjectFactory objectFactory;

        [SetUp]
        public void SetUp()
        {
            this.objectFactory = new DefaultListableObjectFactory();
            IObjectDefinitionReader reader = new XmlObjectDefinitionReader(this.objectFactory);
            reader.LoadObjectDefinitions(new ReadOnlyXmlTestResource("collectionConversionGeneric.xml", GetType()));
        }

        [Test]
        public void ShouldConvertListToGenericIList()
        {
            TestObject to = (TestObject)this.objectFactory.GetObject("HasGenericIListProperty");
            var list = to.SomeGenericIListInt32;
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list[0], Is.EqualTo(123));
            Assert.That(list[1], Is.EqualTo(234));
            Assert.That(list[2], Is.EqualTo(345));
        }

        [Test]
        public void ShouldConvertDictionaryToGenericIDictionary()
        {
            TestObject to = (TestObject)this.objectFactory.GetObject("HasGenericIDictionaryProperty");
            var dict = to.SomeGenericIDictionaryStringInt32;
            Assert.That(dict.Count, Is.EqualTo(3));
            Assert.That(dict["aaa"], Is.EqualTo(111));
            Assert.That(dict["bbb"], Is.EqualTo(222));
            Assert.That(dict["ccc"], Is.EqualTo(333));
        }

        [Test]
        public void ShouldConvertListToGenericIEnumerable()
        {
            TestObject to = (TestObject)this.objectFactory.GetObject("HasGenericIEnumerableProperty");
            var enumerable = to.SomeGenericIEnumerableInt32;
            Assert.That(enumerable.Count(), Is.EqualTo(1));
            Assert.That(enumerable.First(), Is.EqualTo(123));
        }

        [Test]
        public void ConvertArrayListToGenericIList() {
            var xs = new ArrayList { "Mark Pollack" };
            var ys = TypeConversionUtils.ConvertValueIfNecessary(typeof(IList<string>), xs, "ignored");
            Assert.That(ys as IList<string>, Is.Not.Null);
            var zs = (IList<string>)ys;
            Assert.That(zs[0], Is.EqualTo("Mark Pollack"));
        }

        [Test]
        public void ConvertHybridDictionaryToGenericIDictionary()
        {
            var xs = new HybridDictionary { { "first", 1 } };
            var ys = TypeConversionUtils.ConvertValueIfNecessary(typeof(IDictionary<string, int>), xs, "ignored");
            Assert.That(ys as IDictionary<string, int>, Is.Not.Null);
            var zs = (IDictionary<string, int>)ys;
            Assert.That(zs["first"], Is.EqualTo(1));
        }

        [Test]
        public void ConvertArrayListToGenericIEnumerable()
        {
            var xs = new ArrayList { "Mark Pollack" };
            var ys = TypeConversionUtils.ConvertValueIfNecessary(typeof(IEnumerable<string>), xs, "ignored");
            Assert.That(ys as IEnumerable<string>, Is.Not.Null);
            var zs = (IEnumerable<string>)ys;
            var zse = zs.GetEnumerator();
            Assert.That(zse.MoveNext(), Is.True);
            Assert.That(zse.Current, Is.EqualTo("Mark Pollack"));
        }

	}
}
